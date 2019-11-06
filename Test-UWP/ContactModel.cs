using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_UWP.entity;
using Test_UWP.Utils;

namespace Test_UWP
{
    class ContactModel
    {
        public bool Insert(Contact contact )
        {
            try
            {
                using (var stt = SQLiteUtil.GetIntances().Connection.Prepare("INSERT INTO Contact (Name, Phone) VALUES (?, ?)"))
                {
                    stt.Bind(1, contact.Name);
                    stt.Bind(2, contact.Phone);
                  
                    stt.Step();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }
        public Contact GetContactByName(string name)
        {
            try
            {
                using (var stt = SQLiteUtil.GetIntances().Connection.Prepare("SELECT * FROM Contact WHERE Name = ?"))
                {
                    stt.Bind(1, name);
                    if (stt.Step() == SQLiteResult.ROW)
                    {
                        return (new Contact
                        {
                            Name = (string)stt[1],
                            Phone = (long)stt[2],
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public ObservableCollection<Contact> GetListContacts()
        {
            try
            {
                var list = new ObservableCollection<Contact>();
                using (var stt = SQLiteUtil.GetIntances().Connection.Prepare("SELECT * FROM Contact"))
                {

                    while (stt.Step() == SQLiteResult.ROW)
                    {
                        list.Add(new Contact
                        {
                            Name = (string)stt[1],
                            Phone = (long)stt[2],
                        });
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
