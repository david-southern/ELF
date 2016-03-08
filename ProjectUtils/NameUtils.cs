using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectUtils
{
    public class NameUtils
    {
        private static string[] strip_suffixes = 
    {
      "na",
      "n.a.",
      "n.a",
      "inc",
      "inc.",
      "ltd",
      "ltd.",
      "l.t.d.",
      "l.t.d",
      "lp",
      "l.p.",
      "plc",
      "plc.",
      "sa",
      "s.a.",
      "s.a",
      "nv",
      "n.v.",
      "n.v",
      "ag",
      "a.g.",
      "spa",
      "s.p.a.",
      "s.p.a",
      "llc",
      "l.l.c.",
      "l.l.c",
      "co",
      "co.",
      "pte",
      "llp",
      "l.l.p.",
      "l.l.p",
      "gmbh",
      "the",
      "corp",
      "corp."
    };

        private static int min_strip_length, max_strip_length;
        private static Dictionary<string, bool> strip_suffix_dict = null;

        public static string FixupCompanyName(string company_name)
        {
            if (strip_suffix_dict == null)
            {
                strip_suffix_dict = new Dictionary<string, bool>();
                min_strip_length = int.MaxValue;
                max_strip_length = int.MinValue;
                foreach (string suffix in strip_suffixes)
                {
                    strip_suffix_dict[suffix] = true;
                    if (suffix.Length < min_strip_length)
                    {
                        min_strip_length = suffix.Length;
                    }

                    if (suffix.Length > max_strip_length)
                    {
                        max_strip_length = suffix.Length;
                    }
                }

                if (min_strip_length < 1 || max_strip_length > 20 || max_strip_length < min_strip_length)
                {
                    min_strip_length = 0;
                    max_strip_length = 0;
                }
            }

            if (company_name == null)
            {
                return "";
            }

            company_name = company_name.Trim();

            if (company_name.Length < 1)
            {
                return "";
            }

            bool try_again = true;

            while (try_again)
            {
                if (company_name.Length < min_strip_length)
                {
                    return company_name;
                }

                try_again = false;

                if (company_name.EndsWith(")"))
                {
                    int trim_length = company_name.IndexOf('(');

                    if (trim_length > 0)
                    {
                        company_name = company_name.Substring(0, trim_length).Trim();
                        try_again = true;
                    }
                }

                string company_test = company_name.ToLower();

                for (int check_length = max_strip_length; check_length >= min_strip_length; check_length--)
                {
                    if (check_length > company_test.Length)
                    {
                        continue;
                    }

                    int end_index = company_test.Length - check_length;
                    string check_val = company_test.Substring(end_index);

                    if (strip_suffix_dict.ContainsKey(check_val) && (end_index == 0 || !Char.IsLetter(company_name[end_index - 1])))
                    {
                        company_name = company_name.Substring(0, end_index).Trim(" ,.;".ToCharArray());
                        try_again = true;
                        break;
                    }
                }
            }

            company_name = company_name.Replace(" ,", ",");

            return company_name;
        }

        private static string AlphaOnly(string replace_string)
        {
            StringBuilder retval = new StringBuilder();

            for (int i = 0; i < replace_string.Length; i++)
            {
                char ch = replace_string[i];
                if (Char.IsLetter(ch))
                {
                    retval.Append(replace_string[i]);
                }
            }

            return retval.ToString();
        }

        private class FirstNameMatcher
        {
            public static List<string> GetEquivalentNames(string name)
            {
                name = name.ToLower();
                List<int> equiv_list1;
                List<string> returnList = new List<string>();

                equivalence_groups.TryGetValue(name, out equiv_list1);
                if (equiv_list1 == null)
                {
                    returnList.Add(name);
                }
                else
                {
                    foreach (int id in equiv_list1)
                    {
                        List<string> nameList = equivalence_group_map[id];
                        if (nameList != null)
                        {
                            returnList.AddRange(nameList);
                        }
                    }
                }
                return returnList;
            }

            public static bool Matches(string name1, string name2)
            {
                if (name1 == null && name2 == null)
                {
                    return true;
                }

                if (name1 == null || name2 == null)
                {
                    return false;
                }

                name1 = name1.ToLower().Trim();
                name2 = name2.ToLower().Trim();

                if (name1 == name2)
                {
                    return true;
                }

                List<int> equiv_list1, equiv_list2;

                equivalence_groups.TryGetValue(name1, out equiv_list1);
                equivalence_groups.TryGetValue(name2, out equiv_list2);

                if (equiv_list1 != null && equiv_list2 != null)
                {
                    foreach (int this_equiv_group in equiv_list1)
                    {
                        if (equiv_list2.Contains(this_equiv_group))
                        {

                            return true;
                        }
                    }
                }

                return false;
            }

            static FirstNameMatcher()
            {
                AddEquivalent("Abigail Abbie Abby Gail");
                AddEquivalent("Abraham Abram Abe");
                AddEquivalent("Adaline Adeline Adela Addy Ada");
                AddEquivalent("Adolphus Adolph Dolph");
                AddEquivalent("Adrienne Adrian");
                AddEquivalent("Agatha Aggie Agnes Aggy Ag");
                AddEquivalent("Aileen Eileen Helen Lena");
                AddEquivalent("Alan Al");
                AddEquivalent("Alastair Al");
                AddEquivalent("Albert Bert Al");
                AddEquivalent("Alexander Xander Alec Alex Al");
                AddEquivalent("Alexandria Alexandra Xandra Alexa Allie Sandy Alex Alla Alli Ally Lexi Ali Al");
                AddEquivalent("Alfred Alfie Fred Alf Al");
                AddEquivalent("Alicia Alison Alice Allie Elsie Ally Lisa Ali");
                AddEquivalent("Alphonzo Alonzo Lonnie Lonzo Lon");
                AddEquivalent("Amanda Manda Mandy");
                AddEquivalent("Amelia Millie Emily Melia Mel");
                AddEquivalent("Andrew Ander Andy Drew");
                AddEquivalent("Angelica Angelina Angeline Angela Angel Angie Jane");
                AddEquivalent("Annette Annie Anna Anne Ann");
                AddEquivalent("Anthoney Anthony Antonio Antony Tony");
                AddEquivalent("Archibald Archie Baldie Baldo");
                AddEquivalent("Arnold Arnie");
                AddEquivalent("Arthur Artie Art");
                AddEquivalent("Augustina Augustine Augusta Gussie Augie Aggy");
                AddEquivalent("Augustine Augustus Gustavus August Gus");
                AddEquivalent("Barbara Barbie Barbra Bobbie Bonnie Barbi Barby Babs Barb Bab");
                AddEquivalent("Barnard Bernard Barney");
                AddEquivalent("Bartholomew Barth Bart");
                AddEquivalent("Beatrice Trisha Trissy Bea");
                AddEquivalent("Belinda Melinda Linda Lindy Lynn");
                AddEquivalent("Benedict Bennett Bennie Ben");
                AddEquivalent("Benjamin Benjie Benjie Bennie Bengy Benjy Benny Ben");
                AddEquivalent("Bernard Berney Bernie");
                AddEquivalent("Bertram Bert");
                AddEquivalent("Bethany Beth");
                AddEquivalent("Beverley Beverly Bev");
                AddEquivalent("Bradford Bradley Brad");
                AddEquivalent("Bridgitte Bridget Bridgie");
                AddEquivalent("Brittany Britt");
                AddEquivalent("Broderick Brady Brody Ricky");
                AddEquivalent("Bryant Brian Bryan");
                AddEquivalent("Caleb Cal");
                AddEquivalent("Calvin Vinny Cal Vin");
                AddEquivalent("Cameron Kameron Cam");
                AddEquivalent("Camille Cammie Millie");
                AddEquivalent("Carl Karl");
                AddEquivalent("Carla Karla");
                AddEquivalent("Carlotta Karlotta Lottie");
                AddEquivalent("Carmen Carmon Karmen Karmon");
                AddEquivalent("Caroline Carolyn Carole Carol");
                AddEquivalent("Cassandra Cassey Cassie Cassie Sandra Sandi Sandy Cass");
                AddEquivalent("Catharine Catherine Katharine Katherine Cathleen Kathleen Cathlyn Cathryn Kathlyn Kathryn Cathie Kathie Cathi Cathy Kathi Kathy Kathy Cat Kat");
                AddEquivalent("Cecilia Celia Cissy");
                AddEquivalent("Cedric Ricky Rick Ced");
                AddEquivalent("Charles Charley Charlie Chuck Chaz");
                AddEquivalent("Chester Chet");
                AddEquivalent("Chloe Clo");
                AddEquivalent("Christiana Christina Christine Kristine Christy Kristen Crissy Kristy Chris Kris Tina");
                AddEquivalent("Christopher Kristopher Christian Kristian Chris Cris Kris");
                AddEquivalent("Cinthia Cynthia Cindy");
                AddEquivalent("Clarissa Clarice Claire Clara");
                AddEquivalent("Clifford Cliff");
                AddEquivalent("Clifford Ford");
                AddEquivalent("Clifton Cliff");
                AddEquivalent("Clifton Tony");
                AddEquivalent("Conrad Conny Con");
                AddEquivalent("Corey Korey Cory Kory");
                AddEquivalent("Corilee Cora");
                AddEquivalent("Courtney Corky Court Curt");
                AddEquivalent("Curtis Kurtis Curt Kurt");
                AddEquivalent("Cyrus Cy");
                AddEquivalent("Daniel Danny Dan");
                AddEquivalent("Davey David Dave");
                AddEquivalent("Deborah Debbie Debora Debby Debra Debi Deb");
                AddEquivalent("Dennison Dennis Denny");
                AddEquivalent("Derrick Derek Ricky Rick");
                AddEquivalent("Donald Donnie Donny Don");
                AddEquivalent("Douglass Douglas Dougie Doug");
                AddEquivalent("Earnest Ernest Ernie");
                AddEquivalent("Edmund Teddy Ned Ted Ed");
                AddEquivalent("Eduardo Edmund Edward Eddie Edgar Edwin Ed");
                AddEquivalent("Edward Teddy Ned Ted Ed");
                AddEquivalent("Edwin Teddy Ned Ted Ed");
                AddEquivalent("Eleanor Elaine Ellen Helen Lanna Ella");
                AddEquivalent("Eleanor Elaine Lenora Ellen Ellie Nelly Nora");
                AddEquivalent("Elijah Elisha Elija");
                AddEquivalent("Elizabeth Lizabeth Bessie Lizzie Betsy Betty Eliza Lizzy Bess Beth Liza Tess Liz");
                AddEquivalent("Emanuel Manuel Manny");
                AddEquivalent("Eric Erik");
                AddEquivalent("Erwin Irwin");
                AddEquivalent("Eugene Gene Jean");
                AddEquivalent("Eugenia Genie Jenny Gene Jean");
                AddEquivalent("Ezekiel Zeke Ez");
                AddEquivalent("Ezra Ez");
                AddEquivalent("Ferdinand Fernando Ferdie Fred");
                AddEquivalent("Florence Florrie Flora Flo");
                AddEquivalent("Frances Francis Frankie Frank Franz");
                AddEquivalent("Frances Frankie Frannie Cissy Fanny Fran Sis");
                AddEquivalent("Franklin Franklyn Frankie Franki Franky Frank");
                AddEquivalent("Frederick Frederic Fredrick Freddie Fredrik Freddy Fritz Fred");
                AddEquivalent("Gabriel Gabe");
                AddEquivalent("Garbrielle Gabriella Gabrielle Gabbie Gabby Ella");
                AddEquivalent("Garry Gary");
                AddEquivalent("Gefferey Geoffrey Jefferey Geffery Geoffry Jeffery Jeffrey Jeffrie Jeffie Jeffry Geoff Jeffy Jeff");
                AddEquivalent("Gene Jean");
                AddEquivalent("Georges George Georg Jorge");
                AddEquivalent("Gerald Garry Gerry Jerry Gary");
                AddEquivalent("Geraldine Gerrie Gerri Gerry Jerry");
                AddEquivalent("Greggory Gregorio Gregory Gregor Gregg Greg");
                AddEquivalent("Gwendolyn Wendy Gwen");
                AddEquivalent("Hamilton Ham");
                AddEquivalent("Hannah Annie Anna Anne Ann");
                AddEquivalent("Harold Harry Henry Hank Hal");
                AddEquivalent("Helena Helen");
                AddEquivalent("Heloise Eloise Louise");
                AddEquivalent("Herbert Bert Herb");
                AddEquivalent("Howard Howie");
                AddEquivalent("Hubert Bert Hugh Hugo");
                AddEquivalent("Irving Irvin");
                AddEquivalent("Isaac Zeke Ike");
                AddEquivalent("Isabella Isabelle Isabel Bella Belle");
                AddEquivalent("Ivan John");
                AddEquivalent("Jackson Jack");
                AddEquivalent("Jacobus Jacob Jaap Jake Jay");
                AddEquivalent("Jacqueline Jackie");
                AddEquivalent("Janet Janie Jenny Jane Jean");
                AddEquivalent("Janice Jan");
                AddEquivalent("Jennifer Jennie Jenna Jenni Jenny Jenn Jen");
                AddEquivalent("Jessica Jessie Jess");
                AddEquivalent("Jimmie James Jimbo Jimmy Jimi Jim");
                AddEquivalent("Johannah Hannah Joanna Nanny Anna Nan");
                AddEquivalent("John Ian");
                AddEquivalent("Jonathan Jonathon Johnnie Johnny Jonny Jack John Jon");
                AddEquivalent("Joseph Joey Joe");
                AddEquivalent("Joshua Josh");
                AddEquivalent("Judith Judie Judi Judy");
                AddEquivalent("Katie Kate Katy");
                AddEquivalent("Kenneth Kenny Ken");
                AddEquivalent("Kimberlee Kimberley Kimberlie Kimberly Kim");
                AddEquivalent("Laurence Lawrence Larrie Larry");
                AddEquivalent("Leonard Lennie Lenny Leon Len Leo");
                AddEquivalent("Leslie Lester Les");
                AddEquivalent("Lewis Louie Louis Lew Lou");
                AddEquivalent("Lillah Lilah Lilly Lily Lil");
                AddEquivalent("Mack Mac");
                AddEquivalent("Madeline Maddie Maddy");
                AddEquivalent("Mae May");
                AddEquivalent("Marcus Markus Marko Marc Mark");
                AddEquivalent("Margaretta Margaret Gretta Maggie Margie Madge Peggy Marg");
                AddEquivalent("Marjorie Margie Madge Marge");
                AddEquivalent("Martin Marty");
                AddEquivalent("Marvin Mervyn Marv Merv");
                AddEquivalent("Matthew Matt");
                AddEquivalent("Maximillian Max");
                AddEquivalent("Maxwell Max");
                AddEquivalent("Meghan Megan Megy Meg");
                AddEquivalent("Melinda Linda Lindy Mindy Mel");
                AddEquivalent("Melissa Mel");
                AddEquivalent("Melvin Mel");
                AddEquivalent("Micah Mica");
                AddEquivalent("Michael Mikey Mike");
                AddEquivalent("Millie Emily Emma Emmy");
                AddEquivalent("Natalie Nettie Natty");
                AddEquivalent("Natasha Tasha Nat");
                AddEquivalent("Nathaniel Nathan Nate Nat");
                AddEquivalent("Nicholas Nicolas Nicky Nick");
                AddEquivalent("Nickie Nicole Nicky Nikki Cole");
                AddEquivalent("Nicodemus Nicky Nick");
                AddEquivalent("Nicole Nikki Niki");
                AddEquivalent("Oliver Ollie");
                AddEquivalent("Olivia Olive Ollie");
                AddEquivalent("Pamella Pamala Pamela Pamm Pam");
                AddEquivalent("Patricia Patrice Patsy Patti Patty Pat");
                AddEquivalent("Patricia Tricia Trisha Trixie");
                AddEquivalent("Patricio Patrick Paddy Pat");
                AddEquivalent("Paulina Pauline Paula Polly");
                AddEquivalent("Peter Petey Pete");
                AddEquivalent("Philippe Philipp Phillip Philip Phill Phil");
                AddEquivalent("Quincy Quinn Quin");
                AddEquivalent("Randolph Randall Dolph Randy Rafe");
                AddEquivalent("Raymond Ray");
                AddEquivalent("Rebecca Beckie Becca Becka Becki Becky Becky Beck");
                AddEquivalent("Reginald Reggie Reg");
                AddEquivalent("Richard Dickie Richie Dicky Richy Ricky Dick Rich Rick");
                AddEquivalent("Robbie Robert Bobby Robby Robin Bob Rob");
                AddEquivalent("Rodger Roger");
                AddEquivalent("Ronald Ronnie Ronny Ron");
                AddEquivalent("Roxanna Roxanne Roxane Roxie Rox");
                AddEquivalent("Russell Rusty Russ");
                AddEquivalent("Samantha Sammie Sam");
                AddEquivalent("Samuel Sammy Sam");
                AddEquivalent("Sandra Sandy");
                AddEquivalent("Sarah Sara");
                AddEquivalent("Scottie Scotti Scotty Scott");
                AddEquivalent("Sheldon Shelly Shel");
                AddEquivalent("Sidney Sydney Sid Syd");
                AddEquivalent("Silvester Sylvester Sly Syl Si");
                AddEquivalent("Simeon Simon Sim");
                AddEquivalent("Solomon Salmon Solly Zolly Saul Sal Sol");
                AddEquivalent("Stephen Stefan Steven Stevie Steve");
                AddEquivalent("Stewart Stuart Stew Stu");
                AddEquivalent("Sullivan Sully");
                AddEquivalent("Susannah Susanna Susanne Suzanne Sukey Susan Susie Suzie Suki Sue");
                AddEquivalent("Terence Terry");
                AddEquivalent("Teresa Tessie Terry Tyrza Tess");
                AddEquivalent("Terri Terry");
                AddEquivalent("Thaddeus Thad");
                AddEquivalent("Theodore Teddy Theo Ted");
                AddEquivalent("Thomas Tomas Tommy Thom Tom");
                AddEquivalent("Tiffany Tiffy Tiff");
                AddEquivalent("Timothy Timmy Timo Tim");
                AddEquivalent("Valerie Val");
                AddEquivalent("Victor Tory Vick Vic");
                AddEquivalent("Victoria Torrie Toria Vicki Tory");
                AddEquivalent("Vincent Vinson");
                AddEquivalent("Vincent Vinnie Vince Vinny Vin");
                AddEquivalent("Virginia Ginger Virgie Ginny Jinie Virgy");
                AddEquivalent("Vivian Viv Vi");
                AddEquivalent("Wallace Wallie Wally");
                AddEquivalent("Walter Walt");
                AddEquivalent("Wilber Wilbur Will");
                AddEquivalent("Wilfred Willie Will");
                AddEquivalent("William Billie Willie Willie Billy Willy Bill Will");
                AddEquivalent("Woodrow Woody Drew Wood");
                AddEquivalent("Zachariah Zacharias Zachary Zachy Zach Zeke");
            }

            private static int current_equivalence_group = 1;
            private static Dictionary<string, List<int>> equivalence_groups = new Dictionary<string, List<int>>();
            private static Dictionary<int, List<string>> equivalence_group_map = new Dictionary<int, List<string>>();

            private static void AddEquivalent(string names)
            {
                names = names.ToLower();

                string[] names_split = names.Split();

                List<string> equivalenceNameList = null;
                if (equivalence_group_map.ContainsKey(current_equivalence_group))
                {
                    // This should not happen since a new group is created each time this method is called, but we will still protect from it.
                    equivalenceNameList = equivalence_group_map[current_equivalence_group];
                }
                else
                {
                    equivalenceNameList = new List<string>();
                }

                foreach (string this_name in names_split)
                {
                    if (this_name == null || this_name.Length < 1)
                    {
                        continue;
                    }

                    List<int> this_equiv_list;

                    if (equivalence_groups.ContainsKey(this_name))
                    {
                        this_equiv_list = equivalence_groups[this_name];
                    }
                    else
                    {
                        this_equiv_list = new List<int>();
                        equivalence_groups.Add(this_name, this_equiv_list);
                    }

                    this_equiv_list.Add(current_equivalence_group);
                    equivalenceNameList.Add(this_name);
                }

                equivalence_group_map[current_equivalence_group] = equivalenceNameList;
                current_equivalence_group++;
            }
        }

        public static List<string> GetEquivalentNames(string name)
        {
            return FirstNameMatcher.GetEquivalentNames(name);
        }

        public static bool MatchFirstName(string name1, string name2)
        {
            return FirstNameMatcher.Matches(name1, name2);
        }

        public static bool ConvertPersonalName(string full_name,
          out string first_name, out string middle_name, out string last_name)
        {
            // Possible formats:
            // First Last
            // First Middle Last
            // Last, First
            // Last, First Middle

            first_name = null;
            middle_name = null;
            last_name = null;

            if (full_name == null)
            {
                return false;
            }

            full_name = full_name.Trim();

            if (full_name.Length < 1)
            {
                return false;
            }

            int comma_pos = full_name.IndexOf(',');

            if (comma_pos > -1)
            {
                last_name = full_name.Substring(0, comma_pos);
                string name_part = full_name.Substring(comma_pos + 1).Trim();

                int space_pos = name_part.IndexOf(' ');

                if (space_pos > -1)
                {
                    first_name = name_part.Substring(0, space_pos);
                    middle_name = name_part.Substring(space_pos + 1).Trim();
                }
                else
                {
                    first_name = name_part;
                }
            }
            else
            {
                int first_space_pos = full_name.IndexOf(' ');
                int last_space_pos = full_name.LastIndexOf(' ');

                if (first_space_pos == -1)
                {
                    last_name = full_name;
                }
                else
                {
                    first_name = full_name.Substring(0, first_space_pos);
                    last_name = full_name.Substring(last_space_pos + 1);

                    if (first_space_pos != last_space_pos)
                    {
                        middle_name = full_name.Substring(first_space_pos + 1, last_space_pos - first_space_pos - 1).Trim();
                    }
                }
            }

            return true;
        }
    }
}
