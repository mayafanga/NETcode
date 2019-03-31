 

using BAR.TeamManager.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.DALFactory
{
    public partial class AbstractFactory
    {
      
   
		
	    public static IactivitieapplicationDal createactivitieapplicationDal()
        {

		 string fullClassName = NameSpace + ".activitieapplicationDal";
          return CreateInstance(fullClassName) as IactivitieapplicationDal;

        }
		
	    public static IactivityDal createactivityDal()
        {

		 string fullClassName = NameSpace + ".activityDal";
          return CreateInstance(fullClassName) as IactivityDal;

        }
		
	    public static IadministratorDal createadministratorDal()
        {

		 string fullClassName = NameSpace + ".administratorDal";
          return CreateInstance(fullClassName) as IadministratorDal;

        }
		
	    public static IauditDal createauditDal()
        {

		 string fullClassName = NameSpace + ".auditDal";
          return CreateInstance(fullClassName) as IauditDal;

        }
		
	    public static IauthorityDal createauthorityDal()
        {

		 string fullClassName = NameSpace + ".authorityDal";
          return CreateInstance(fullClassName) as IauthorityDal;

        }
		
	    public static IcommentDal createcommentDal()
        {

		 string fullClassName = NameSpace + ".commentDal";
          return CreateInstance(fullClassName) as IcommentDal;

        }
		
	    public static IeventapplicantDal createeventapplicantDal()
        {

		 string fullClassName = NameSpace + ".eventapplicantDal";
          return CreateInstance(fullClassName) as IeventapplicantDal;

        }
		
	    public static IhonorDal createhonorDal()
        {

		 string fullClassName = NameSpace + ".honorDal";
          return CreateInstance(fullClassName) as IhonorDal;

        }
		
	    public static IhonorparticipantmemberDal createhonorparticipantmemberDal()
        {

		 string fullClassName = NameSpace + ".honorparticipantmemberDal";
          return CreateInstance(fullClassName) as IhonorparticipantmemberDal;

        }
		
	    public static IparticipatmembersDal createparticipatmembersDal()
        {

		 string fullClassName = NameSpace + ".participatmembersDal";
          return CreateInstance(fullClassName) as IparticipatmembersDal;

        }
		
	    public static IpersonalinformationDal createpersonalinformationDal()
        {

		 string fullClassName = NameSpace + ".personalinformationDal";
          return CreateInstance(fullClassName) as IpersonalinformationDal;

        }
		
	    public static IplayersDal createplayersDal()
        {

		 string fullClassName = NameSpace + ".playersDal";
          return CreateInstance(fullClassName) as IplayersDal;

        }
		
	    public static IregisterloginDal createregisterloginDal()
        {

		 string fullClassName = NameSpace + ".registerloginDal";
          return CreateInstance(fullClassName) as IregisterloginDal;

        }
		
	    public static IscoreDal createscoreDal()
        {

		 string fullClassName = NameSpace + ".scoreDal";
          return CreateInstance(fullClassName) as IscoreDal;

        }
		
	    public static IteacherDal createteacherDal()
        {

		 string fullClassName = NameSpace + ".teacherDal";
          return CreateInstance(fullClassName) as IteacherDal;

        }
		
	    public static IteamDal createteamDal()
        {

		 string fullClassName = NameSpace + ".teamDal";
          return CreateInstance(fullClassName) as IteamDal;

        }
		
	    public static IteamapplicantDal createteamapplicantDal()
        {

		 string fullClassName = NameSpace + ".teamapplicantDal";
          return CreateInstance(fullClassName) as IteamapplicantDal;

        }
		
	    public static IteamapplicationDal createteamapplicationDal()
        {

		 string fullClassName = NameSpace + ".teamapplicationDal";
          return CreateInstance(fullClassName) as IteamapplicationDal;

        }
		
	    public static IuserDal createuserDal()
        {

		 string fullClassName = NameSpace + ".userDal";
          return CreateInstance(fullClassName) as IuserDal;

        }
		
	    public static IuserauthorityDal createuserauthorityDal()
        {

		 string fullClassName = NameSpace + ".userauthorityDal";
          return CreateInstance(fullClassName) as IuserauthorityDal;

        }
		
	    public static IwebmasterDal createwebmasterDal()
        {

		 string fullClassName = NameSpace + ".webmasterDal";
          return CreateInstance(fullClassName) as IwebmasterDal;

        }
	}
	
}