using System;

namespace SignalTransformation.Core
{

    public static class Tester
    {
        public static string Test(double[] array, double[] correct, double a, int s)
        {
            const double Threshold = 1E-15;
            var calculated = Transformer.Transform(array, a, s);

            // this approach does not work out of the box because even if each and every
            // element of Sequence the same, whole Sequence is different, so I have two choices:
            // * implement DoubleComparer class and use it in Enumerable.SequenceEqual(calculated, correct, new DoubleComparer())
            // * or use manual one-by-one sequence comparison (simple and easy, but not the best option)
            //return Enumerable.SequenceEqual(calculated, correct) ? "test pass" : "test fail";

            bool result = true;
            for (int i = 0; i < correct.Length; i++)
            {
                if (Math.Abs(correct[i] - calculated[i]) > Threshold)
                {
                    result = false;
                }
            }
            return result ? "test pass" : "test fail";
        }

        public static double[] GenerateArray(int amplitude, int size)
        {
            double[] result = new double[size];
            var rnd = new Random();

            for (int i = 0; i < size; i++)
            {
                result[i] = rnd.Next(0, amplitude);
            }

            return result;
        }
    }
}
