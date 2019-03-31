 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BAR.TeamManager.IDAL
{
	public partial interface IDBSession
    {

	
		IactivitieapplicationDal activitieapplicationDal{get;set;}
	
		IactivityDal activityDal{get;set;}
	
		IadministratorDal administratorDal{get;set;}
	
		IauditDal auditDal{get;set;}
	
		IauthorityDal authorityDal{get;set;}
	
		IcommentDal commentDal{get;set;}
	
		IeventapplicantDal eventapplicantDal{get;set;}
	
		IhonorDal honorDal{get;set;}
	
		IhonorparticipantmemberDal honorparticipantmemberDal{get;set;}
	
		IparticipatmembersDal participatmembersDal{get;set;}
	
		IpersonalinformationDal personalinformationDal{get;set;}
	
		IplayersDal playersDal{get;set;}
	
		IregisterloginDal registerloginDal{get;set;}
	
		IscoreDal scoreDal{get;set;}
	
		IteacherDal teacherDal{get;set;}
	
		IteamDal teamDal{get;set;}
	
		IteamapplicantDal teamapplicantDal{get;set;}
	
		IteamapplicationDal teamapplicationDal{get;set;}
	
		IuserDal userDal{get;set;}
	
		IuserauthorityDal userauthorityDal{get;set;}
	
		IwebmasterDal webmasterDal{get;set;}
	}	
}