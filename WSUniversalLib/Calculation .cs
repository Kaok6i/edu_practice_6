using System;

namespace WSUniversalLib
{
    public class Calculation
    {
        /// <summary>
        /// Расчет количества необходимого сырья для производства продукций
        /// </summary>
        /// <param name="productType"></param>
        /// <param name="materialType"></param>
        /// <param name="count"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public int GetQuantityForProduct(int productType,int materialType,int count,float width,float length)
        {
            if (productType <= 3 && productType > 0 
                && width > 0 && length > 0 
                && materialType <= 2 && materialType >0 
                && count >= 1)
            {
                // Нахождение площади создаваемой продукций
                var area = width * length;
                double productFactor = 0;
                double faultFactor;
                double quantity;
                // Определение процента брака материла
                if (materialType == 1)
                {
                    faultFactor = 0.003;
                }
                else
                {
                    faultFactor = 0.012;
                }
                // Определение коэфициента типа продукций
                switch (productType)
                {
                    case 1:
                        productFactor = 1.1;
                        break;
                    case 2:
                        productFactor = 2.5;
                        break;
                    case 3:
                        productFactor = 8.43;
                        break;
                }
                // Расчет необходимого сырья 
                quantity = (area * count * productFactor) / (1 - 0.003);
                return Convert.ToInt32(Math.Ceiling(quantity));
            }
            else
            {
                return -1;
            }
        }
    }
}
