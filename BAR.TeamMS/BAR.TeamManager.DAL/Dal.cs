 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.IDAL;
using BAR.TeamManager.Model;
using System.Linq.Expressions;

namespace BAR.TeamManager.DAL
{
		
	public partial class activitieapplicationDal :BaseDal<activitieapplication>,IactivitieapplicationDal
    {

    }
		
	public partial class activityDal :BaseDal<activity>,IactivityDal
    {

    }
		
	public partial class administratorDal :BaseDal<administrator>,IadministratorDal
    {

    }
		
	public partial class auditDal :BaseDal<audit>,IauditDal
    {

    }
		
	public partial class authorityDal :BaseDal<authority>,IauthorityDal
    {

    }
		
	public partial class commentDal :BaseDal<comment>,IcommentDal
    {

    }
		
	public partial class eventapplicantDal :BaseDal<eventapplicant>,IeventapplicantDal
    {

    }
		
	public partial class honorDal :BaseDal<honor>,IhonorDal
    {

    }
		
	public partial class honorparticipantmemberDal :BaseDal<honorparticipantmember>,IhonorparticipantmemberDal
    {

    }
		
	public partial class participatmembersDal :BaseDal<participatmembers>,IparticipatmembersDal
    {

    }
		
	public partial class personalinformationDal :BaseDal<personalinformation>,IpersonalinformationDal
    {

    }
		
	public partial class playersDal :BaseDal<players>,IplayersDal
    {

    }
		
	public partial class registerloginDal :BaseDal<registerlogin>,IregisterloginDal
    {

    }
		
	public partial class scoreDal :BaseDal<score>,IscoreDal
    {

    }
		
	public partial class teacherDal :BaseDal<teacher>,IteacherDal
    {

    }
		
	public partial class teamDal :BaseDal<team>,IteamDal
    {

    }
		
	public partial class teamapplicantDal :BaseDal<teamapplicant>,IteamapplicantDal
    {

    }
		
	public partial class teamapplicationDal :BaseDal<teamapplication>,IteamapplicationDal
    {

    }
		
	public partial class userDal :BaseDal<user>,IuserDal
    {

    }
		
	public partial class userauthorityDal :BaseDal<userauthority>,IuserauthorityDal
    {

    }
		
	public partial class webmasterDal :BaseDal<webmaster>,IwebmasterDal
    {

    }
	
}