using System;

namespace SignalTransformation.Core
{
    public static class Transformer
    {
        public static double[] Transform(double[] given_array, double new_amplitude, int new_size)
        {
            double[] new_array = new double[new_size];
            double[] middle = new double[given_array.Length * new_size];
            double given_array_max = 0;

            for (int i = 0; i < given_array.Length; i++)
            {
                if (given_array[i] > given_array_max)
                {
                    given_array_max = given_array[i];
                }
            }

            if (given_array_max == 0)
                throw new ArgumentException("Given input array must contains at least one element greater than zero");

            // building new intermediate buffer (array)
            // this algorythm is not optimal from memory consumption point of view
            // but it gives the best approximation
            int index = 0;
            foreach (var item in given_array)
            {
                for (int i = 0; i < new_size; i++)
                {
                    middle[index * new_size + i] = item;
                }
                index++;
            }

            index = 0;
            for (int i = 0; i < new_size; i++)
            {
                double calc_v = 0;

                for (int j = 0; j < given_array.Length; j++)
                {
                    calc_v = calc_v + middle[index * given_array.Length + j];
                }
                index++;

                new_array[i] = (new_amplitude / given_array_max) * (calc_v / given_array.Length);
            }
            
            return new_array;
        }
    }
}
