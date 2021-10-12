﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagementLib.DataAccess;

namespace BookManagementLib
{
    class ReportDAO
    {
        private static ReportDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ReportDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ReportDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Report> GetReportList()
        {
            var Reports = new List<Report>();
            try
            {
                using var context = new BookManagementDBContext();
                Reports = context.Reports.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Reports;
        }

        public IEnumerable<Report> GetReportsBycompanyID(string companyID)
        {
            var Reports = new List<Report>();
            try
            {
                using var context = new BookManagementDBContext();
                Reports = context.Reports.Where(r => r.CompanyId == companyID).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Reports;
        }

        public IEnumerable<Report> GetReportsByProductID(string proID)
        {
            var Reports = new List<Report>();
            try
            {
                using var context = new BookManagementDBContext();
                Reports = (List<Report>)context.Reports.OrderByDescending(r => r.ProductId == proID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Reports;
        }

        public Report GetReportByID(int id)
        {
            Report Report = null;
            try
            {
                using var context = new BookManagementDBContext();
                Report = context.Reports.SingleOrDefault(m => m.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Report;
        }
        public void Insert(Report Report)
        {
            try
            {
                Report supRep = GetReportByID(Report.Id);
                if (supRep == null)
                {
                    using var context = new BookManagementDBContext();
                    context.Reports.Add(Report);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Report is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Report Report)
        {
            try
            {
                Report supRep = GetReportByID(Report.Id);
                if (supRep != null)
                {
                    using var context = new BookManagementDBContext();
                    context.Reports.Update(Report);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Report does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int ReportID)
        {
            try
            {
                Report supRep = GetReportByID(ReportID);
                if (supRep != null)
                {
                    using var context = new BookManagementDBContext();
                    context.Reports.Remove(supRep);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Report does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int IdGenerate()
        {
            int newid = 0;
            try
            {
                using var context = new BookManagementDBContext();
                Report report = context.Reports.ToList().OrderByDescending(r => r.Id).FirstOrDefault();
                if (report == null)
                {
                    return newid++;
                }
                else
                {
                    newid = report.Id++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return newid;
        }
    }
}