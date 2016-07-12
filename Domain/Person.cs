﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Domain
{
    public enum Gender { M, F };
    public abstract class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string AdressHome { get; set; }
        public string PhoneNumber { get; set; }

        //it is calculable properties with Expression Body Members
        public int Age => (int) (DateTime.Now - BirthDate).Days/365;  
        //{
        //    DateTime now = DateTime.Today;
        //    int  age = now.Year - BirthDate.Year;
        //    if (now < BirthDate.AddYears(age)) age--;
        //    return age;
        //}

    }
}