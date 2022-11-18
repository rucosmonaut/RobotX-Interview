using Microsoft.AspNetCore.Components.Forms;
using RobotX_Interview.Entities;
using System.Diagnostics;
using System.Xml;

namespace RobotX_Interview.Data
{
    public class ClientService
    {
        private readonly ApplicationContext DataBase;

        public ClientService(ApplicationContext dataBase)
        {
            DataBase = dataBase;
        }

        public Client? GetClient(long cardCode)
        {
            return DataBase.Clients.FirstOrDefault(c => c.CardCode == cardCode);
        }

        public IEnumerable<Client> GetClients()
        {
            return DataBase.Clients;
        }

        public bool DeleteClient(Client delClient)
        {
            Client? client = DataBase.Clients.Find(delClient);

            if(client != null)
            {
                DataBase.Clients.Remove(client);
                DataBase.SaveChanges();

                return true;
            }

            return false;
        }

        public void CreateClient(Client client)
        {
            DataBase.Clients.Add(client);
            DataBase.SaveChanges();
        }

        public void CreateClients(IEnumerable<Client> clients)
        {
            DataBase.Clients.AddRange(clients);
            DataBase.SaveChanges();
        }

        public void EditClient(Client editClient)
        {
            DataBase.Clients.Update(editClient);
            DataBase.SaveChanges();
        }

        public async Task<bool> CreateClientsFromXML(IBrowserFile file)
        {
            try
            {
                var process = Process.GetCurrentProcess();
                string fullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string path = Path.Combine(fullPath, $"client_{Guid.NewGuid()}.xml");

                await using FileStream fs = new(path, FileMode.Create);
                await file.OpenReadStream().CopyToAsync(fs);
                fs.Close();

                XmlDocument xDoc = new ();
                xDoc.Load(path);
                XmlElement? xRoot = xDoc.DocumentElement;

                if (xRoot != null)
                {
                    foreach (XmlElement xnode in xRoot)
                    {
                        var CardCode = Convert.ToInt64(xnode.Attributes.GetNamedItem("CARDCODE")?.Value);
                        Client client = new ()
                        {
                            CardCode = Convert.ToInt64(xnode.Attributes.GetNamedItem("CARDCODE")?.Value),
                            StartDate = xnode.Attributes.GetNamedItem("STARTDATE")?.Value == ""
                                ? null : Convert.ToDateTime(xnode.Attributes.GetNamedItem("STARTDATE")?.Value),
                            FinishDate = xnode.Attributes.GetNamedItem("FINISHDATE")?.Value == ""
                                ? null : Convert.ToDateTime(xnode.Attributes.GetNamedItem("FINISHDATE")?.Value),
                            LastName = xnode.Attributes.GetNamedItem("LASTNAME")?.Value,
                            FirstName = xnode.Attributes.GetNamedItem("FIRSTNAME")?.Value,
                            SurName = xnode.Attributes.GetNamedItem("SURNAME")?.Value,
                            Gender = xnode.Attributes.GetNamedItem("GENDER")?.Value,
                            BirthDay = xnode.Attributes.GetNamedItem("BIRTHDAY")?.Value == ""
                                ? null : Convert.ToDateTime(xnode.Attributes.GetNamedItem("BIRTHDAY")?.Value),
                            PhoneHome = xnode.Attributes.GetNamedItem("PHONEHOME")?.Value ?? "",
                            PhoneMobil = xnode.Attributes.GetNamedItem("PHONEMOBIL")?.Value ?? "",
                            Email = xnode.Attributes.GetNamedItem("EMAIL")?.Value ?? "",
                            City = xnode.Attributes.GetNamedItem("CITY")?.Value ?? "",
                            Street = xnode.Attributes.GetNamedItem("STREET")?.Value ?? "",
                            House = xnode.Attributes.GetNamedItem("HOUSE")?.Value ?? "",
                            Apartment = xnode.Attributes.GetNamedItem("APARTMENT")?.Value ?? "",
                        };

                        CreateClient(client);
                    }
                    File.Delete(path);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}