    using System;
    using System.Globalization;
    using System.Numerics;


public class DSAClass
{
    /* 0 has to be prepended to the string, otherwise a negative number is the ouptu, if the first digit is between 8-F */
    private static string stringp = "086F5CA03DCFEB225063FF830A0C769B9DD9D6153AD91D7CE27F787C43278B447" +
                                    "E6533B86B18BED6E8A48B784A14C252C5BE0DBF60B86D6385BD2F12FB763ED88" +
                                    "73ABFD3F5BA2E0A8C0A59082EAC056935E529DAF7C610467899C77ADEDFC846C" +
                                    "881870B7B19B2B58F9BE0521A17002E3BDD6B86685EE90B3D9A1B02B782B1779";
    private static BigInteger p = BigInteger.Parse(stringp, NumberStyles.AllowHexSpecifier);

    private static string stringq = "0996F967F6C8E388D9E28D01E205FBA957A5698B1";
    private static BigInteger q = BigInteger.Parse(stringq, NumberStyles.AllowHexSpecifier);

    /* Generator */
    private static string string_g = "07B0F92546150B62514BB771E2A0C0CE387F03BDA6C56B505209FF25FD3C133D" +
                                     "89BBCD97E904E09114D9A7DEFDEADFC9078EA544D2E401AEECC40BB9FBBF78FD" +
                                     "87995A10A1C27CB7789B594BA7EFB5C4326A9FE59A070E136DB77175464ADCA4" +
                                     "17BE5DCE2F40D10A46A3A3943F26AB7FD9C0398FF8C76EE0A56826A8A88F1DBD";
    private static BigInteger gen = BigInteger.Parse(string_g, NumberStyles.AllowHexSpecifier);

    /* Private key 160 Bit */
    private static string x = "411602CB19A6CCC34494D79D98EF1E7ED5AF25F7";
    private static BigInteger privx = BigInteger.Parse(x, NumberStyles.AllowHexSpecifier);

    /* Public key 1024 Bit*/
    private static string y = "5DF5E01DED31D0297E274E1691C192FE5868FEF9E19A84776454B100CF16F653" +
                              "92195A38B90523E2542EE61871C0440CB87C322FC4B4D2EC5E1E7EC766E1BE8D" +
                              "4CE935437DC11C3C8FD426338933EBFE739CB3465F4D3668C5E473508253B1E6" +
                              "82F65CBDC4FAE93C2EA212390E54905A86E2223170B44EAA7DA5DD9FFCFB7F3B";
    private static BigInteger puby = BigInteger.Parse(y, NumberStyles.AllowHexSpecifier);
    private static BigInteger r = BigInteger.Zero, s = BigInteger.Zero, v = BigInteger.Zero;
    private static byte[] hashtext = null;


    static int Main (string[] args)
    {
        //Console.WriteLine("y={0}", puby);

        System.Security.Cryptography.HashAlgorithm secHash = new System.Security.Cryptography.SHA256CryptoServiceProvider();
        
        BigInteger alpha = InitDSA();
        Sign(alpha, secHash);
        Console.WriteLine("\nSignature:\nr = {0}\ns = {1}", r, s);
        Boolean result = VerifyObject(alpha, secHash);
        

        /* Final Output */
        if (result)
        {
            Console.WriteLine("SUCCESS!!!\nSignature is valid!");
        } else
        {
            Console.WriteLine("FAILED!!!\nSignature is invalid!");
        }
        Console.WriteLine("Press the any key...");
        Console.ReadKey();
        return (0);
    }


    private static BigInteger InitDSA()
    {
        /* Alpha */
        BigInteger exponent = BigInteger.Divide((BigInteger.Subtract(p, 1)), q);
        BigInteger alpha = BigInteger.ModPow(gen, exponent, p);
/* FALSCHER WERT VON ALPHA */
        BigInteger test = BigInteger.ModPow(alpha, privx, p);
        //Console.WriteLine("test: {0}\n y: {1}", test, puby);
        return alpha;
    }


    private static void Sign(BigInteger alpha, System.Security.Cryptography.HashAlgorithm secHash)
    {
        
        BigInteger proof = BigInteger.Zero, k = BigInteger.Zero, invertk = BigInteger.Zero;

        /* Get random k and invers of k */
        do
        {
            k = MRT.getRandomPositiveBigInteger(159, true);
            invertk = ExtendedEuclidianAlgo.ExtendedEuclid(k, q);
            proof = (BigInteger.Multiply(k, invertk)) % q;
        } while (proof != 1);

        /* User input */
        Console.Write("Please enter your text here: ");
        string input = Console.ReadLine();
        byte[] asciitext = System.Text.Encoding.ASCII.GetBytes(input);
        Console.WriteLine("\nStarted signing...");

        /* Hashing of user input */
        hashtext = secHash.ComputeHash(asciitext);
        BigInteger hashValue = new BigInteger(hashtext);
        
        /* r = ((alpha^k) mod p) mod q */
        r = BigInteger.ModPow(alpha, k, p) % q;

        /* s = k^-1 {h(m)+xr} mod q */
        s = BigInteger.Multiply(invertk, (BigInteger.Add(hashValue, BigInteger.Multiply(privx, r)))) % q;
    }


    private static bool VerifyObject(BigInteger alpha, System.Security.Cryptography.HashAlgorithm secHash)
    {
        Console.WriteLine("\nVerification of signature started...");
        if (r < q && s < q)
        {
            /* Calculate w and h(m) */
            BigInteger w = ExtendedEuclidianAlgo.ExtendedEuclid(q, s);
            BigInteger hashValue = new BigInteger(hashtext);

 /* FEHLER BEI HASHVALUE - WERT WIRD NICHT GESPEICHERT*/           /* Calculate u_one and u_two */
            BigInteger uone = BigInteger.Multiply(w, hashValue) % q;
            BigInteger utwo = BigInteger.Multiply(r, w) % q;

            /* Calculation of v */
            //BigInteger tempalpha = BigInteger.ModPow(alpha, uone, p);
            //BigInteger tempy = BigInteger.ModPow(puby, utwo, p);
            BigInteger v = BigInteger.Multiply(BigInteger.ModPow(alpha, uone, p), BigInteger.ModPow(puby, utwo, p)) % q;
            Console.WriteLine("\nVerification:\nv = {0}", v);

            if (BigInteger.Compare(v, r) == 0)
            {
                return true;
            }
        }
        return false;
    }
}

/*

    DSA 1024 Bits

    Key pair:

    key parameters:

    p = 86F5CA03DCFEB225063FF830A0C769B9DD9D6153AD91D7CE27F787C43278B447
        E6533B86B18BED6E8A48B784A14C252C5BE0DBF60B86D6385BD2F12FB763ED88
        73ABFD3F5BA2E0A8C0A59082EAC056935E529DAF7C610467899C77ADEDFC846C
        881870B7B19B2B58F9BE0521A17002E3BDD6B86685EE90B3D9A1B02B782B1779

    q = 996F967F6C8E388D9E28D01E205FBA957A5698B1

    g = 07B0F92546150B62514BB771E2A0C0CE387F03BDA6C56B505209FF25FD3C133D
        89BBCD97E904E09114D9A7DEFDEADFC9078EA544D2E401AEECC40BB9FBBF78FD
        87995A10A1C27CB7789B594BA7EFB5C4326A9FE59A070E136DB77175464ADCA4
        17BE5DCE2F40D10A46A3A3943F26AB7FD9C0398FF8C76EE0A56826A8A88F1DBD

    private key:

    x = 411602CB19A6CCC34494D79D98EF1E7ED5AF25F7

    public key:

    y = 5DF5E01DED31D0297E274E1691C192FE5868FEF9E19A84776454B100CF16F653
        92195A38B90523E2542EE61871C0440CB87C322FC4B4D2EC5E1E7EC766E1BE8D
        4CE935437DC11C3C8FD426338933EBFE739CB3465F4D3668C5E473508253B1E6
        82F65CBDC4FAE93C2EA212390E54905A86E2223170B44EAA7DA5DD9FFCFB7F3B

    Signatures:

    With SHA-1, message = "sample":
    k = 7BDB6B0FF756E1BB5D53583EF979082F9AD5BD5B
    r = 2E1A0C2562B2912CAAF89186FB0F42001585DA55
    s = 29EFB6B0AFF2D7A68EB70CA313022253B9A88DF5

    With SHA-224, message = "sample":
    k = 562097C06782D60C3037BA7BE104774344687649
    r = 4BC3B686AEA70145856814A6F1BB53346F02101E
    s = 410697B92295D994D21EDD2F4ADA85566F6F94C1

    With SHA-256, message = "sample":
    k = 519BA0546D0C39202A7D34D7DFA5E760B318BCFB
    r = 81F2F5850BE5BC123C43F71A3033E9384611C545
    s = 4CDD914B65EB6C66A8AAAD27299BEE6B035F5E89

    With SHA-384, message = "sample":
    k = 95897CD7BBB944AA932DBC579C1C09EB6FCFC595
    r = 07F2108557EE0E3921BC1774F1CA9B410B4CE65A
    s = 54DF70456C86FAC10FAB47C1949AB83F2C6F7595

    With SHA-512, message = "sample":
    k = 09ECE7CA27D0F5A4DD4E556C9DF1D21D28104F8B
    r = 16C3491F9B8C3FBBDD5E7A7B667057F0D8EE8E1B
    s = 02C36A127A7B89EDBB72E4FFBC71DABC7D4FC69C

    With SHA-1, message = "test":
    k = 5C842DF4F9E344EE09F056838B42C7A17F4A6433
    r = 42AB2052FD43E123F0607F115052A67DCD9C5C77
    s = 183916B0230D45B9931491D4C6B0BD2FB4AAF088

    With SHA-224, message = "test":
    k = 4598B8EFC1A53BC8AECD58D1ABBB0C0C71E67297
    r = 6868E9964E36C1689F6037F91F28D5F2C30610F2
    s = 49CEC3ACDC83018C5BD2674ECAAD35B8CD22940F

    With SHA-256, message = "test":
    k = 5A67592E8128E03A417B0484410FB72C0B630E1A
    r = 22518C127299B0F6FDC9872B282B9E70D0790812
    s = 6837EC18F150D55DE95B5E29BE7AF5D01E4FE160

    With SHA-384, message = "test":
    k = 220156B761F6CA5E6C9F1B9CF9C24BE25F98CD89
    r = 854CF929B58D73C3CBFDC421E8D5430CD6DB5E66
    s = 91D0E0F53E22F898D158380676A871A157CDA622

    With SHA-512, message = "test":
    k = 65D2C2EEB175E370F28C75BFCDC028D22C7DBE9C
    r = 8EA47E475BA8AC6F2D821DA3BD212D11A3DEB9A0
    s = 7C670C7AD72B6C050C109E1790008097125433E8
   
*/
  