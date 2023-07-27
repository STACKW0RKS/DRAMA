namespace DRAMA.Helpers.Cryptography;

public static class CertificateHelpers
{
    public static bool CertificateIsInstalled(string certificateThumbprint, StoreName storeName = StoreName.Root, StoreLocation storeLocation = StoreLocation.CurrentUser)
    {
        X509Store store = new(storeName, storeLocation);

        store.Open(OpenFlags.ReadOnly);

        X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);

        return certificates.Any() ? true : false;
    }

    public static void InstallCertificate(string certificateFile, StoreName storeName = StoreName.Root, StoreLocation storeLocation = StoreLocation.CurrentUser)
    {
        using (X509Store store = new(storeName, storeLocation))
        {
            store.Open(OpenFlags.ReadWrite);
            X509Certificate2 certificate = new(X509Certificate.CreateFromCertFile(certificateFile));
            store.Add(certificate);
        }
    }

    public static void UninstallCertificate(string certificateThumbprint, StoreName storeName = StoreName.Root, StoreLocation storeLocation = StoreLocation.CurrentUser)
    {
        if (CertificateIsInstalled(certificateThumbprint))
        {
            X509Store store = new(storeName, storeLocation);

            store.Open(OpenFlags.ReadWrite);

            X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);

            foreach (X509Certificate2 certificate in certificates)
                store.Remove(certificate);
        }
    }
}
