using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Kontrol_2_Server.Classes
{
    // Some parts of the code are from AForge Documentation example on VideoFileWriter Class

    internal class VideoMaker
    {
        public Bitmap ToBitmap(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            var bitmap = new Bitmap(returnImage);

            return bitmap;
        }

        public Bitmap ReduceBitmap(Bitmap original, int reducedWidth, int reducedHeight)
        {
            var reduced = new Bitmap(reducedWidth, reducedHeight);
            using (var dc = Graphics.FromImage(reduced))
            {
                // you might want to change properties like
                dc.InterpolationMode = InterpolationMode.HighQualityBicubic;
                dc.DrawImage(original, new Rectangle(0, 0, reducedWidth, reducedHeight), new Rectangle(0, 0, original.Width, original.Height), GraphicsUnit.Pixel);
            }

            return reduced;
        }

        private void CreateMovie(DateTime startDate, DateTime endDate)
        {
            int width = 320;
            int height = 240;
            var framRate = 200;

            /*using (var container = new ImageEntitiesContainer())
            {
                //a LINQ-query for getting the desired images
                var query = from d in container.ImageSet
                            where d.Date >= startDate && d.Date <= endDate
                            select d;

                // create instance of video writer
                using (var vFWriter = new VideoFileWriter())
                {
                    // create new video file
                    vFWriter.Open("nameOfMyVideoFile.avi", width, height, framRate, VideoCodec.Raw);

                    var imageEntities = query.ToList();

                    //loop throught all images in the collection
                    foreach (var imageEntity in imageEntities)
                    {
                        //what's the current image data?
                        var imageByteArray = imageEntity.Data;
                        var bmp = ToBitmap(imageByteArray);
                        var bmpReduced = ReduceBitmap(bmp, width, height);

                        vFWriter.WriteVideoFrame(bmpReduced);
                    }
                    vFWriter.Close();
                }
            }*/
        }
    }
}
