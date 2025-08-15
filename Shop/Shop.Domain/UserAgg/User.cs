using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.UserAgg
{
    public class User : AggregateRoot
    {
        public User(string name, string family, string phoneNumber, string email, string password, Gender gender, IDomainUserService domainService)
        {
            Guard(phoneNumber, email, domainService);

            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            Gender = gender;
        }

        public string Name { get; private set; }
        public string Family { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Password { get; set; }
        public Gender Gender { get; private set; }
        public List<UserRole> Roles { get; private set; }
        public List<Wallet> Wallets { get; private set; }
        public List<UserAddress> Addresses { get; private set; }



        public void Edit(string name, string family, string phoneNumber,  string email, string password, Gender gender , IDomainUserService domainService)
        {
            Guard(phoneNumber, email , domainService);
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            Gender = gender;
            Password = password;
        }

        public static User RegisterUser(string email , string phoneNumber , string password , IDomainUserService domainService)
        {
            return new User("", "", phoneNumber, email, password, Gender.none, domainService);
        }
        public void AddAddress(UserAddress address)
        {
            Addresses.UserId = Id;
            Addresses.Add(address);
        }
        public void EditAddress(UserAddress address)
        {
            var oldAddress = Addresses.FirstOrDefault(a=>a.Id==address.Id);
            if (oldAddress == null)
                throw new NullOrEmptyDomainDataException("address not found");
            Addresses.Remove(oldAddress);
            Addresses.Add(address);
        }
        public void DeleteAddress(long addressId)
        {
            var Address = Addresses.FirstOrDefault(a => a.Id == addressId);
            if (Address == null)
                throw new NullOrEmptyDomainDataException("address not found");
            Addresses.Remove(Address);
        }

        public void ChargeWallet(Wallet wallet)
        {
            Wallets.Add(wallet);
        }

        public void SetRoles(List<UserRole> roles)
        {
            roles.ForEach(r=>r.UserId = Id);
            Roles.Clear();
            Roles.AddRange(roles);
        }

        public void Guard(string phoneNumber, string email , IDomainUserService domainService)
        {
            NullOrEmptyDomainDataException.CheckString(email , nameof(email));
            NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
            if (phoneNumber.Length != 11)
                throw new InvalidDomainDataException("شماره تلفن نامعتبر است");
            if(email.IsValidEmail() == false)
                throw new InvalidDomainDataException("ایمیل نامعتبر است");

            if(phoneNumber!=PhoneNumber)
                if(domainService.PhoneNumberIsExist(phoneNumber) == true)
                    throw new InvalidDomainDataException("شماره تلفن تکراری است");

            if (email != Email)
                if (domainService.IsEmailExist(email) == true)
                    throw new InvalidDomainDataException("ایمیل تکراری است");


        }
    }
}
