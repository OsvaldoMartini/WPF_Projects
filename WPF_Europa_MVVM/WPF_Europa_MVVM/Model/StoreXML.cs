using System;
using System.Collections.Generic;
using WPF_Europa_MVVM.Controls;
using WPF_Europa_MVVM.ViewModels;

namespace WPF_Europa_MVVM.Model
{
    public class StoreXML
    {
        public bool hasError = false;
        public string errorMessage;

        public bool DeleteUser(int p)
        {
            try
            {
                return new UserModel().WhitUserId(p).DeleteByXML();

            }
            catch (Exception ex)
            {
                errorMessage = "Update error, " + ex.Message;
                hasError = true;
            }

            return (!hasError);
        }//DeleteUserByXML


        public bool SaveUser(UserVM UserToDisplay)
        {
            try
            {
                return new UserModel(UserToDisplay).AddByXML();

            }
            catch (Exception ex)
            {
                errorMessage = "Update error, " + ex.Message;
                hasError = true;
            }

            return (!hasError);
        }//SaveUserByXML

        public bool UpdateUser(UserVM displayP)
        {

            try
            {
                return new UserModel(displayP).UpdateByXML();

            }
            catch (Exception ex)
            {
                errorMessage = "Update error, " + ex.Message;
                hasError = true;
            }

            return (!hasError);
        }//UpdateUserByXML


        public UserObservableCollection<UserVM> GetUsers()
        {
            hasError = false;
            UserObservableCollection<UserVM> products = new UserObservableCollection<UserVM>();
            try
            {
                //True: Check if File Exist
                products = new UserModel().WhitCheckFileFirst(true);

            } //try
            catch (Exception ex)
            {
                errorMessage = "GetUsers() error, " + ex.Message;
                hasError = true;
            }

            return products;
        }//GetUsersByXML


        public List<RoleModel> GetRoles()
        {
            hasError = false;
            List<RoleModel> roles= new List<RoleModel>();
            try
            {
                //True: Check if File Exist
                roles = DBUtility.MockRoles();

            } //try
            catch (Exception ex)
            {
                errorMessage = "GetRoles() error, " + ex.Message;
                hasError = true;
            }

            return roles;
        }//GetRoles

        public List<DeptoModel> GetDepartments()
        {
            hasError = false;
            List<DeptoModel> departments = new List<DeptoModel>();
            try
            {
                departments = DBUtility.MockDepartment();

            } //try
            catch (Exception ex)
            {
                errorMessage = "GetDepartments() error, " + ex.Message;
                hasError = true;
            }

            return departments;
        }//GetDepartments


    }
} 

