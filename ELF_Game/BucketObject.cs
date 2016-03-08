using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ELF
{
    class BucketObject
    {
        public Bitmap theImage {get; set;}
        public int imageValue {get; set;}

        public BucketObject(Bitmap b, int v)
        {
            theImage = b;
            imageValue = v;
        }
    }
}
