using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Text.RegularExpressions;

namespace ProjectUtils
{
  public class WebUtils
  {
    private static readonly string META_REFRESH_PATTERN = "<meta http-equiv=\"Refresh\" content=\"\\d+;URL=([^\"]*)\">";

    public static string PreventXSS(string raw_input)
    {
      return HttpUtility.HtmlEncode(raw_input);
    }

    public static string DownloadURL(string url)
    {
      return DownloadURL(url, null, true, null);
    }

    public static string DownloadURL(string url, string post_vars)
    {
      return DownloadURL(url, post_vars, true, null);
    }

    public static string DownloadURL(string url, string post_vars, bool follow_redirects, CookieContainer cookies)
    {
      return DownloadURLWorker(url, post_vars, follow_redirects, cookies, 0);
    }

    private static string DownloadURLWorker(string url, string post_vars, bool follow_redirects,
        CookieContainer cookies, int recursion_count)
    {
      if (recursion_count++ > 5)
      {
        return "Too many auto-refresh recursions";
      }


      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

      /* Here is a vanilla set of headers that I get from Forefox 2.0.0.12.  We'll use
       * these so that we look as much like a standard browser as possible.
       */

      request.Accept = "text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
      request.Headers.Add("Accept-Language", "en-us,en;q=0.5");
      request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
      // request.Headers.Add("Accept-Encoding", "gzip,deflate");
      request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.12) Gecko/20080201 Firefox/2.0.0.12";
      request.AllowAutoRedirect = follow_redirects;
      request.MaximumAutomaticRedirections = 50;

      if (cookies != null)
      {
        request.CookieContainer = cookies;
      }

      if (post_vars != null)
      {
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";

        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] postParams = encoding.GetBytes(post_vars);

        request.ContentLength = postParams.Length;

        Stream postStream = request.GetRequestStream();
        postStream.Write(postParams, 0, postParams.Length);
        postStream.Close();
      }
      else
      {
        request.Method = "GET";
      }

      HttpWebResponse response = (HttpWebResponse)request.GetResponse();

      Stream dataStream = response.GetResponseStream();
      string contents;

      if (response.ContentEncoding == "gzip")
      {
        GZipStream unzipper = new GZipStream(dataStream, CompressionMode.Decompress);
        StreamReader reader = new StreamReader(unzipper);
        contents = reader.ReadToEnd();
        reader.Close();
        unzipper.Close();
      }
      else if (response.ContentEncoding == "deflate")
      {
        DeflateStream deflater = new DeflateStream(dataStream, CompressionMode.Decompress);
        StreamReader reader = new StreamReader(deflater);
        contents = reader.ReadToEnd();
        reader.Close();
        deflater.Close();
      }
      else
      {
        StreamReader reader = new StreamReader(dataStream);
        contents = reader.ReadToEnd();
        reader.Close();
      }

      if (cookies != null)
      {
        cookies.Add(response.Cookies);
      }

      if (follow_redirects)
      {
        // Some pages may contain a meta-tag that specifies an immegiate refresh to a different 
        // URL, something like this:
        // 
        //  <meta http-equiv="Refresh" 
        //    content="0;URL=/addressBookExport?exportNetworkRedirect=&outputType=microsoft_outlook">
        //
        // In this case, we need to send another request to the URL specified in the request in order to
        // actually get the page data.

        string[] matches;

        if (FindRegexMatches(contents, META_REFRESH_PATTERN, out matches))
        {
          // Don't forget to HtmlDecode the string, as the ampersand in the URL in the example above
          // will be represented as &amp; in the actual content returned.
          string refresh_url = HttpUtility.HtmlDecode(matches[0]).Trim();

          if (refresh_url.StartsWith("/"))
          {
            int slash_pos = url.IndexOf('/');

            if (slash_pos >= 0)
            {
              refresh_url = url.Substring(0, slash_pos) + refresh_url;
            }
          }

          contents = DownloadURLWorker(refresh_url, null, true, cookies, recursion_count++);
        }
      }

      return contents;
    }

    private static Dictionary<string, Regex> regex_cache = new Dictionary<string, Regex>();

    public static bool FindRegexMatches(string raw_data, string regex_pattern,
            out Regex find_pattern, out Match[] matches)
    {
      matches = null;
      find_pattern = null;

      lock (regex_cache)
      {
        if (regex_cache.ContainsKey(regex_pattern))
        {
          find_pattern = regex_cache[regex_pattern];
        }
        else
        {
          find_pattern = new Regex(regex_pattern);
          regex_cache.Add(regex_pattern, find_pattern);
        }
      }

      MatchCollection pattern_match = find_pattern.Matches(raw_data);

      if (pattern_match.Count > 0)
      {
        matches = new Match[pattern_match.Count];
        pattern_match.CopyTo(matches, 0);
        return true;
      }

      return false;
    }

    public static bool FindRegexMatches(string raw_data, string regex_pattern, out string[] match_strings)
    {
      match_strings = null;

      Regex result_pattern;
      Match[] matches;

      if (FindRegexMatches(raw_data, regex_pattern, out result_pattern, out matches))
      {
        match_strings = new string[matches.Length];

        int string_index = 0;

        foreach (Match this_match in matches)
        {
          if (this_match.Groups.Count > 1)
          {
            match_strings[string_index++] = this_match.Groups[1].Value;
          }
          else if (this_match.Groups.Count > 0)
          {
            match_strings[string_index++] = this_match.Groups[0].Value;
          }
        }

        return true;
      }

      return false;
    }

    public static string GetHTMLFromTemplate(string template_name, Dictionary<string, string> replacements)
    {
        if (HttpContext.Current == null)
        {
            throw new InvalidOperationException("GetHTMLFromTemplate called without a HttpContext");
        }

        string template_path = HttpContext.Current.Server.MapPath(String.Format("~/templates/{0}.html", template_name));

        StreamReader file_stream = new StreamReader(File.Open(template_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        string return_text = file_stream.ReadToEnd();
        file_stream.Close();

        string site_url = HttpContext.Current.Request.Url.AbsoluteUri;
        string path = HttpContext.Current.Request.Url.AbsolutePath;

        site_url = site_url.Remove(site_url.LastIndexOf(path));
        site_url += HttpContext.Current.Request.ApplicationPath;

        site_url = site_url.TrimEnd("/".ToCharArray());

        return_text = return_text.Replace("{**site_url**}", site_url);

        foreach (string this_key in replacements.Keys)
        {
            string this_value = replacements[this_key];

            return_text = return_text.Replace(this_key, this_value);
        }

        return return_text;
    }



  }
}
