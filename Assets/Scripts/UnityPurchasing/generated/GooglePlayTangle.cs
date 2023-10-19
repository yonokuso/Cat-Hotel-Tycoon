// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("3IgS2KvqORnrwGl0GEKJyy3g8aQ5wu/igwZuc6dZ0Xy/e6ojp8u/6NQaio7pj91jI42THLVA0g8LBnqlmhZPOH8eIld55eVtSdeMh75lZofefMVfFsGdo0dWdhOTiiP9JjfECAGL6ey1cPGHwUvWp3fWNRxKOuXyz9tQVP0qm3iGLqaCP/cwrRK/F+SMPr2ejLG6tZY69DpLsb29vbm8v/nQOvjM/z/jbzk8M0hHNz22NHzkPr2zvIw+vba+Pr29vB2VeevLLrMk8BKUxXDyjn6AUpFOz5PnEVDJ/4BanJK/FbW2/xPP+QwFcYM5133AslpKoF8MJNUqJ7rSZthsDqTbNQNLGZAH/2/l4EuMnvBCl4eqMo1MlepVOJ6aI7fFLb6/vby9");
        private static int[] order = new int[] { 7,8,9,5,5,13,12,7,12,12,13,12,13,13,14 };
        private static int key = 188;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
