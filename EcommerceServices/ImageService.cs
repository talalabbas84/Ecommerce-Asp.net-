using EcommerceDatabase;
using EcommerceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceServices
{
    public class ImageService
    {
        public void SaveImage(Image img)
        {
            using (var context = new Db())
            {

                context.Images.Add(img);
                context.SaveChanges();
            }
        }


        public void UpdateImage(Image image)
        {
            using (var context = new Db())
            {
                
                if (image.ID == 0)
                {
                    List<Image> tempImage = context.Images.ToList();
                    image.ID = tempImage.ElementAt(tempImage.Count - 1).ID + 1;
                    SaveImage(image);
                }
                else
                {
                    context.Entry(image).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }


        public void DeleteImage(int itemID)
        {
            using (var context = new Db())
            {
                // context.Entry(category).State = System.Data.Entity.EntityState.Deleted;

                List<Image> images = context.Images.Where(x=> x.ItemID.Equals(itemID)).ToList();

                foreach(Image imgs in images)
                {
                context.Images.Remove(imgs);
                context.SaveChanges();

                }

            }

        }


        public List<Image> GetImages(int ID)
        {
            List<Image> images;
            using (var context = new Db())
            {
                images = context.Images.Where(x => x.ItemID == ID).ToList();
            }
            return images;
        }
    }
}
