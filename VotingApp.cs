using System;
using System.Collections.Generic;
using System.Linq;

namespace voting_uygulamasi
{
    public class VotingApp
    {
        public VotingApp()
        {
            categories = new List<Category>();
            categories.AddRange( new List<Category> 
            {
                new Category { CategoryName="Film", NumberOfVote=1},
                new Category { CategoryName="Tech", NumberOfVote=3},
                new Category { CategoryName="Spor", NumberOfVote=2}
            });

            users = new List<User>();
            for (int i = 1; i <= 6; i++)
            {
                users.Add(new User { Name="testAD_"+i, Surname="testSurname_"+i, UserName="testUsername_"+i, Voted = true});
            }
        }
        private List<Category> categories;
        private List<User> users;

        public void Vote()
        {
            while (true)
            {

                ListCategory();

                User user = null;

                Console.Write($"Oy vermek istediğiniz kategoriyi seçiniz (1-{categories.Count} veya iptal için '0') : ");

                if (int.TryParse(Console.ReadLine(), out int selectedCategory) && (selectedCategory>=0 && selectedCategory <= categories.Count))
                {
                    if (selectedCategory == 0)
                    {
                        Console.WriteLine("Oy verme işlemi iptal edildi.");
                        break;
                    }
                    else
                    {
                        user = Login();

                        if (user != null)
                        {
                            if (user.Voted == false)
                            {
                                categories[selectedCategory-1].NumberOfVote++;
                            }
                            else
                            {
                                Console.WriteLine("Zaten oy kullandınız. Tekrar kullanamazsınız.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Giriş yapmadığınız için oy kullanamıyorsunuz.");
                        }
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"Hatalı seçim yaptınız. Lütfen 0-{categories.Count} arası bir seçim yapınız.");
                }


            }

            ShowVotingResult();

        }

        private void ListCategory()
        {
            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"{i+1}- {categories[i].CategoryName} Kategorisi");
            }
        }

        private User Login()
        {
            
            User user = null;

            while (true)
            {
                Console.Write("Kullanıcı adınızı giriniz (Abone olmak için '1', iptal etmek için '2') : ");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    user = Register();
                    break;
                }
                else if (input == "2")
                {
                    Console.WriteLine("Giriş işlemi iptal edildi.");
                    break;
                }
                else
                {
                    if (IsRegisteredUser(input))
                    {
                        user = users.FirstOrDefault(user=>user.UserName == input);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Kullanıcı adı bulunamadı. Tekrar deneyin.");
                    }
                }
            }

            return user;
        }

        private bool IsRegisteredUser(string userName)
        {
            bool isRegistered = false;

            foreach (User user in users)
            {
                if(user.UserName == userName)
                    isRegistered = true;
            }

            return isRegistered;
        }

        private User Register()
        {
            User user = null;
            Console.Write("Adınızı giriniz : ");
            string name = Console.ReadLine();
            Console.Write("Soyadınızı giriniz : ");
            string surname = Console.ReadLine();

            while (true)
            {
                Console.Write("Kullanıcı adınızı giriniz (iptal için '1'): ");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    Console.WriteLine("Kayıt olma işlemi iptal edildi.");
                    break;
                }
                if (IsRegisteredUser(input))
                {
                    Console.WriteLine("Bu kullanıcı adı zaten var. Tekrar deneyin.");
                }
                else
                {
                    user = new User { Name = name, Surname = surname, UserName = input, Voted = false};
                    users.Add(user);
                    break;
                }
            }

            return user;
        }
    
        private void ShowVotingResult()
        {
            Console.WriteLine("***** Oylama Sonuçları *****");
            

            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"{i+1} - {categories[i].CategoryName} Kategorisi -> Oy sayısı : {categories[i].NumberOfVote}, Oy yüzdesi : % {CalculatePersentage(categories[i].NumberOfVote)}");
            }

        }

        private double CalculatePersentage(int numberOfVote)
        {
            int totalvote = 0;
            foreach (Category category in categories)
            {
                totalvote += category.NumberOfVote;
            }

            return Math.Round((100 * (double)numberOfVote) / totalvote, 2);
        }

    }
}