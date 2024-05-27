using System.Linq;

public static class LicensePlateValidator
{
    public static bool IsValidSouthAfricanLicensePlate(string licensePlate)
    {
        // Define the list of valid prefixes for South African license plates
        var validPrefixes = new[]
        {
            "CA", "CAM", "CAR", "CAW", "CAG", "CBL", "CBM", "CBR", "CBS", "CBT", "CBY", "CCA", "CCC", "CCD", "CCK",
            "CCM", "CCO", "CCP", "CEA", "CEG", "CEM", "CEO", "CER", "CES", "CEX", "CEY", "CF", "CFA", "CFG", "CFM",
            "CFP", "CFR", "CG", "CJ", "CK", "CL", "CN", "CO", "CR", "CS", "CT", "CV", "CW", "CX", "CY", "CZ", "CCT",
            "NA", "NB", "NBA", "NC", "NCO", "NCH", "NCW", "ND", "NDE", "NDH", "NDW", "NE", "NES", "NF", "NGL", "NH",
            "NHL", "NIM", "NIN", "NIP", "NIX", "NJ", "NK", "NKA", "NKK", "NKR", "NKU", "NM", "NMA", "NMG", "NMR",
            "NMZ", "NN", "NND", "NO", "NP", "NPG", "NPN", "NPP", "NR", "NRB", "NS", "NPS", "NSC", "NT", "NTU", "NU",
            "NUB", "NUD", "NUF", "NUL", "NUM", "NUR", "NUT", "NUZ", "NV", "NW", "NX", "NZ", "B", "DBN", "EC", "FS",
            "GP", "KZN", "L", "MP", "NC", "NW", "ZA"
        };

        // Check if the license plate starts with a valid prefix
        var prefix = licensePlate.Split(' ').First();
        if (!validPrefixes.Contains(prefix))
        {
            return false;
        }

        // Additional validation logic can be added if needed

        // If the license plate passes all checks, return true
        return true;
    }
}
