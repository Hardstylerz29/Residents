using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZS.Common
{
    #region Utilities & Helpers

    /// <summary>
    /// Parameters for GetImages request
    /// </summary>
    public class GetImagesParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Defines which type of image you are requesting. 
        /// 1: Seniors
        /// 2: Wound pictures. (v1)
        /// 3: Signature Doctor
        /// 4: Wound Pictures (v2)
        /// </summary>
        public short ImageType;
        /// <summary>
        /// Filter: if specified only image for this linked object will retrieved. (Eg specific resident ID if you only need
        /// that resident's image. If left null, images for all known linked objects will be retrieved. (eg. all resident items.) 
        /// </summary>
        public string ImageLinkID;
        /// <summary>
        /// Required image size.
        /// 'tiny': 50x50px
        /// 'small': 150x150px
        /// 'large':800x800 px
        /// </summary>
        public string ImageSize;
    }
    /// <summary>
    /// Parameters for GetImageStatus request.
    /// </summary>
    public class GetImageStatusParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Defines which type of image you are requesting the status for
        /// 1: Seniors
        /// 2: Wound pictures. (v1)
        /// 3: Signature Doctor
        /// 4: Wound Pictures (v2)
        /// /// </summary>
        public short ImageType;
        /// <summary>
        /// Filter: if specified only image status for this linked object will retrieved. (E.g. specific resident ID if you only need
        /// that resident's image. If left null, image status for all known linked objects will be retrieved. (e.g. all resident items.) 
        /// </summary>
        public int? LinkID;
        /// <summary>
        /// Filter: can be used to retrieve only the images modified s after set date. If left null, status of all images will be retrieved.
        /// </summary>
        public DateTime? ModifiedSince;
    }
    /// <summary>
    /// Parameters for GetText request
    /// </summary>
    public class GetTextParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// ID to retrieve text for.
        /// </summary>
        public int TextID;
        /// <summary>
        /// Language to receive text in.
        /// 0: User's session language. (Default language specified for that user account in CS software)
        /// 1: Dutch
        /// 2: French
        /// </summary>
        public byte LanguageID;
    }
    /// <summary>
    /// Parameters for UploadDBImage request
    /// </summary>
    public class UploadDBImageParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to image type you are uploading.
        /// 1: Seniors
        /// 2: Wound pictures. (v1)
        /// 3: Signature Doctor
        /// 4: Wound Pictures (v2)
        /// </summary>
        public short ImageType;
        /// <summary>
        /// Set to Unique ID of the object you are uploading an image for. (Resident ID, Wound ID,...)
        /// </summary>
        public int ImageLinkID;
        /// <summary>
        /// Base 64 encoded JPEG file data. (Currently only Jpeg images are supported.)
        /// </summary>
        public string ImageData;
    }
    
    /// <summary>
    /// Parameters for DateTimeToCaremoment request
    /// </summary>
    public class DateTimeToCaremomentParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// DateTime value you wish to convert to a CareDate and Caremoment object. (DateTimeAsCaremoment Class)
        /// </summary>
        public DateTime TimeStamp;
    }
    /// <summary>
    /// Parameters for GetModifiedObject request
    /// </summary>
    public class GetModifiedObjectsParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Object type you want to request the status of. (String)
        /// "USERS" -> get a list of user's and their last modification date. (or create date if not modified)
        /// "SENIORS" -> get a list of user's and their last modification date. (or create date if not modified)
        /// </summary>
        public string ObjectType;
        /// <summary>
        /// Filter: can be used to retrieve only the objects modified s after set date. If left null, all objects will be retrieved.
        /// </summary>
        public DateTime? ModifiedSince;

    }
    /// <summary>
    /// Parameters for GetCountryCodes method
    /// </summary>
    public class GetCountryCodesParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: set to a countrycode’s ID value if you only want to request a single country code. Leave null to retrieve
        /// a full list of known countrycodes.
        /// </summary>
        public Int16? CountryCodeID;
    }

    /// <summary>
    /// Parameters for GetLookupValues method
    /// </summary>
    public class GetLookupValuesParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: set to a comma separated list of Type ID's to retrieve only LookupValues for the chosen type(s). Leave null to retrieve
        /// a full list of LookupValues for a single Language.
        /// </summary>
        public string TypeIDs;
        /// <summary>
        /// Filter: set to a single ValueID to retrieve only LookupValues for the chosen ValueID. We encourage to use this in combination with a TypeID parameter filter.
        /// Leave null to retrieve all possible values for specified type.
        /// </summary>
        public int? ValueID;
        /// <summary>
        ///Set to the languageID in which you would like the LookUpValues to be returned. 0 for dutch, 1 for french. Leave null to use the current session's language.
        /// </summary>
        public int? LanguageID;
    }

    #endregion Utilities & Helpers

    #region Webservice technical
    #endregion Webservice technical

    #region Login

    /// <summary>
    /// Parameters for Login Method
    /// </summary>
    public class LoginParams
    {
        /// <summary>
        /// Set this property to: User's initials + "@" + Target's database DisplayName.
        /// 
        /// User's initials -> (username) as set in his CS Software user account. (WZD/WZM)
        /// Target's database Displayname -> Displayname of the database you want the user to log in to. Every 
        /// care home has a separate database. The list of databases your application has access to can be retrieved
        /// using the GetWZDDatbases or Connections method. (Login Controller)
        /// 
        /// E.g. If active user John Doe (user initials in WZD: "JD") wants to log in to database with displayname 
        /// 'SilverMeadows', his aUserID parameter should be set to: "JD@SilverMeadows'
        /// </summary>
        public string aUserID;
        /// <summary>
        /// User Password
        /// </summary>
        public string aPassword;
        /// <summary>
        /// ApplicationKey of your application. (These are hardcoded and managed by Care Solutions. These keys are created
        /// by request. You can contact us if you need an Application Key for your application.
        /// </summary>
        public string aApplicationKey;
    }

    /// <summary>
    /// Parameters for LoginUserByExtID request
    /// </summary>
    public class LoginUserByExtIdParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string sessionid;
        /// <summary>
        /// External ID of the user that wants to log in. (E.g. badge number as configured in CS Software)
        /// -> See User class' IButton property 
        /// </summary>
        public string externalId;
        /// <summary>
        /// ID Of the WZD Database you want to Login to. (See WZDatabase class.)
        /// </summary>
        public short? databaseID;
        /// <summary>
        /// ID of the desired session. If left null, default user language will be used.
        /// 1 to force Dutch, 2 to force French.
        /// </summary>
        public byte? languageID;
    }

    /// <summary>
    /// Parameters for LoginValidatedUserByEmail request
    /// </summary>
    public class LoginValidatedUserByEmailParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string sessionid;
        /// <summary>
        /// Email address (as set in the CS software) of the validated user that wants to log in.
        /// </summary>
        public string userEmail;
        /// <summary>
        /// ID Of the WZD Database you want to Login to. (See WZDatabase class.)
        /// </summary>
        public short? databaseID;
        /// <summary>
        /// ID of the desired session. If left null, default user language will be used.
        /// 1 to force Dutch, 2 to force French.
        /// </summary>
        public byte? languageID;
    }

    /// <summary>
    /// Parameters for LoginValidatedUserByUserName
    /// </summary>
    public class LoginValidatedUserByUserNameParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string sessionid;
        /// <summary>
        /// User Initials (username) as configured for this user's user account. (CS Software)
        /// </summary>
        public string userName;
        /// <summary>
        /// ID Of the WZD Database you want to Login to. (See WZDatabase class.)
        /// </summary>
        public short? databaseID;
        /// <summary>
        /// ID of the desired session. If left null, default user language will be used.
        /// 1 to force Dutch, 2 to force French.
        /// </summary>
        public byte? languageID;
    }

    /// <summary>
    /// Parameters for GetWZDatabases request
    /// </summary>
    public class GetWZDatabasesParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string sessionid;
    }

    #endregion Login

    #region Rooms and Departments

    /// <summary>
    /// Parameters for GetDepartmentList request
    /// </summary>
    public class GetDepartmentListParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
    }

    /// <summary>
    /// Parameters for GetRoomlist request
    /// </summary>
    public class GetRoomListParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// filter: use this property to only retrieve the rooms in a certain department.
        /// If you want to filter, set this property to the Department's ID you wish to retrieve
        /// the rooms for. Leave NULL to retrieve all rooms for all departments.
        /// </summary>
        public short? DepartmentID;
    }

    /// <summary>
    /// Parameters for GetRoomOccupancyStatus and GeShortStayRoomOccupancyStatus
    /// </summary>
    public class GetRoomOccupancyParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Begin date of period you wish to retrieve the room occupancy for
        /// </summary>
        public DateTime StartDate;
        /// <summary>
        /// End of period you wish to retrieve the room occupancy for
        /// </summary>
        public DateTime EndDate;
        /// <summary>
        /// Filter: use this property to only retrieve the occupancy for rooms in a certain department.
        /// If you want to filter, set this property to the Department's ID you wish to retrieve
        /// the room occupancy for. Leave NULL to retrieve occupancy for rooms in all departments.
        /// </summary>
        public int? DepartmentID;
        /// <summary>
        /// Filter: Set to the unique ID of a room to only retrieve occupancy for selected room. Leave NULL
        /// to retrieve occupancy status of all rooms.
        /// </summary>
        public int? RoomID;
    }

    /// <summary>
    /// Parameters for GetRoomstyles method
    /// </summary>
    public class GetRoomstylesParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;        
    }

    /// <summary>
    /// Parameters for GetRoomtypes method
    /// </summary>
    public class GetRoomtypesParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
    }


    #endregion Rooms and Departments

    #region Residents & Resident Info

    /// <summary>
    /// Parameters for GetContactForResident request
    /// </summary>
    public class GetContactsForResidentParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to resident ID you want to retrieve contacts for.
        /// </summary>
        public int ResidentID;
    }

    /// <summary>
    /// Parameters for GetResidentDiseasesParameters
    /// </summary>
    public class GetResidentDiseasesParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: set to Resident ID if you only want to retrieve ResidentDiseases for a single residentd. 
        /// Leave NULL to retrieve ResidentDiseases for all residents.
        /// </summary>
        public int? ResidentID;
        /// <summary>
        /// Filter: set to a Date to retrieve all ResidentDiseases since set date. 
        /// Leave NULL to retrieve all ResidentDiseases
        /// </summary>
        public DateTime? BeginDate;
        /// <summary>
        /// Filter: set to a Date to retrieve all ResidentDiseases up until set date. 
        /// Leave NULL to retrieve all ResidentDiseases
        /// </summary>
        public DateTime? EndDate;
        /// <summary>
        /// Set true to only show active ResidentDiseases. (Not cured, still ongoing.)
        /// Set to false to retrieve all ResidentDiseases. (Including inactive, cured diseases.)
        /// </summary>
        public Boolean ShowActiveOnly;
    }

    /// <summary>
    /// Parameters for GetResidentMedicalRemarks
    /// </summary>
    public class GetResidentMedicalRemarksParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: set to Resident ID if you only want to retrieve ResidentMedicalRemarks for a single ResidentID. 
        /// Leave NULL to retrieve ResidentMedicalRemarks for all residents.
        /// </summary>
        public int? ResidentID;
        /// <summary>
        /// Filter: set to Department ID if you only want to retrieve ResidentMedicalRemarks for residents in a certain 
        /// department. Leave NULL to retrieve ResidentMedicalRemarks for residents in all departments.
        /// </summary>
        public short? DepartmentID;
        /// <summary>
        /// Filter: set to Room ID if you only want to retrieve ResidentMedicalRemarks for a residents in certain room. 
        /// Leave NULL to retrieve ResidentMedicalRemarks for residents in all rooms.
        /// </summary>
        public int? RoomID;
        /// <summary>
        /// Filter: set to True if you want to retrieve ResidentMedicalRemarks for active residents only. Set to False 
        /// to retrieve ResidentMedicalRemarks for inactive residents only. Leave NULL to retrieve ResidentMedicalRemarks
        /// for both Active and Inactive residents.
        /// </summary>
        public bool? Active;
    }

    /// <summary>
    /// Parameters for GetResidentParams request
    /// </summary>
    public class GetResidentParamsParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to comma separated ID List of all the residents you wish to retrieve the parameters for. (E.g. "1203,586,889775")
        /// </summary>
        public string ResidentID;
        /// <summary>
        /// Filter: Set to Unique ID of the parameter type you wish to lookup. (See ParamList class)
        /// If left null all parametertypes will be retrieved.
        /// </summary>
        public int? ParamListID;
        /// <summary>
        /// Filter: set to a Date to retrieve all ResidentParams after set date. 
        /// Leave NULL to retrieve all ResidentParams
        /// </summary>
        public DateTime? DateFrom;
        /// <summary>
        /// Filter: set to a Date to retrieve all ResidentParams until set date. 
        /// Leave NULL to retrieve all ResidentParams
        /// </summary>
        public DateTime? DateUntil;
        /// <summary>
        /// Filter: set to a number 'n' to retrieve the last 'n' registered parameters for each Paramater type per resident. E.g. Last 3 registered
        /// blood pressure values.
        /// Leave NULL to retrieve all registered ResidentParams per resident per type.
        /// </summary>
        public byte? GetLastNr;
    }

    /// <summary>
    /// Parameters of GetResidents request
    /// </summary>
    public class GetResidentsParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: set to Department ID if you only want to retrieve the residents in a certain 
        /// department. Leave NULL to retrieve residents in all departments.
        /// </summary>
        public int? DepartmentID;
        /// <summary>
        /// Filter: set to Room ID if you only want to retrieve residents in certain room. 
        /// Leave NULL to retrieve residents in all rooms.
        /// </summary>
        public int? RoomID;
        /// <summary>
        /// Filter: set to Resident ID if you only want to retrieve info of set resident.
        /// Leave NULL to retrieve info for all residents.
        /// </summary>
        public int? ResidentID;
        /// <summary>
        /// Filter: set to true if you want to retrieve only active residents. Set to false
        /// if you only want to retrieve inactive residents. Set to NULL if you want to retrieve
        /// both active and inactive residents.
        /// </summary>
        public bool? Active;
    }

    /// <summary>
    /// Parameters for GetResidentTreatmentDirectives request
    /// </summary>
    public class GetResidentTreatmentDirectivesParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to resident ID you wish to retrieve the ResidentTreatmentDirectives for.
        /// </summary>
        public int ResidentID;
    }

    /// <summary>
    /// Parameters for InsertKatz method
    /// </summary>
    public class InsertKatzParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to Resident ID you wish to register a Katz Score for. If registering a WaitingListKatz, set to 0
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// Set to WaitingListEntry ID you wish to register a Katz Score for. If registering a Resident Katz, set to 0
        /// </summary>
        public int WaitingListEntryID;        
        /// <summary>
        /// Date when Katz was scored.
        /// </summary>
        public DateTime BeginDate;
        /// <summary>
        /// Set to Resident's scored Katz for Washing
        /// </summary>
        public byte KatzWash;
        /// <summary>
        /// Set to Resident's scored Katz for Clothing
        /// </summary>
        public byte KatzCloth;
        /// <summary>
        /// Set to Resident's scored Katz for Movement
        /// </summary>
        public byte KatzMove;
        /// <summary>
        /// Set to Resident's scored Katz for Toilet
        /// </summary>
        public byte KatzToilet;
        /// <summary>
        /// Set to Resident's scored Katz for Continence
        /// </summary>
        public byte KatzCont;
        /// <summary>
        /// Set to Resident's scored Katz for Food
        /// </summary>
        public byte KatzFood;
        /// <summary>
        /// Set to Resident's scored Katz for Time awareness
        /// </summary>
        public byte KatzTime;
        /// <summary>
        /// Set to Resident's scored Katz for Location awareness
        /// </summary>
        public byte KatzPlace;
    }

    /// <summary>
    /// Parameters for GetResidentVaccinations
    /// </summary>
    /// 
    public class GetResidentVaccinationsParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to comma separated ID List of all the residents you wish to retrieve the ResidentVaccination info for. (E.g. "1203,586,889775")
        /// </summary>
        public String ResidentIDList;
        /// <summary>
        /// Filter: set to a VaccinationTypeID to only receive vaccination ResidentVaccination info of the requested VaccinationType.
        /// Leave NULL to receive ResidentVaccinations of all VaccinationTypes.
        /// </summary>
        public int? VaccinationTypeID;
        /// <summary>
        /// Filter: Set to True if you only want to receive the last given vaccination info per type. Set to False to receive all.
        /// </summary>
        public bool GetLastVaccOnly;
    }

    /// <summary>
    /// Parameters for InsertResidentVaccinationParams
    /// </summary>
    public class InsertResidentVaccinationParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to ResidentID you want to register a new vaccination for.
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// Set to the VaccinationTypeID of the vaccine you are registering.
        /// </summary>
        public int VaccinationTypeID;
        /// <summary>
        /// Any remarks you want to register for this vaccination can be set here.
        /// </summary>
        public string Remarks;
        /// <summary>
        /// Set to Begin date of the vaccination. (When you leave this parameter NULL, the current system date will be used)
        /// </summary>
        public DateTime? VaccBeginDate; //Indien null wordt de huidige datum gebruikt
        /// <summary>
        /// Sets the expiry date of this Resident's vaccination. 
        /// If left null the system will automatically calculate the default expiry date for this vaccination by using the 
        /// MonthsExpiry property of the set VaccinationType. (If configured in CS software... Else it will be registered as null)
        /// </summary>
        public DateTime? VaccExpiryDate; //Indien null wordt zelf de expiry datum berekend volgens vaccinatietype (geldigheid in maanden)
    }

    /// <summary>
    /// Parameters for UploadResidentDocument request.
    /// </summary>
    public class InsertResidentDocumentParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Resident ID of the resident you are inserting a document for.
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// ID of the documenttype you are uploading. For available types use the GetResidentDocumentTypes method.
        /// </summary>
        public short DocumentTypeID;
        /// <summary>
        /// Title/Name of the document as you want it to appear in the resident's document list. (eg. Intake letter,...)
        /// </summary>
        public string DocumentName;
        /// <summary>
        /// Extension of the file you are uploading. Used to determine the type of the document. (pdf, doc, xls,...)
        /// only pass the extension, no '.' prefix.
        /// </summary>
        public string FileExtension;
        /// <summary>
        /// File as base64 encoded string.
        /// </summary>
        public string FileDataAsB64;
    }

    /// <summary>
    /// Parameters for GetResidentDocumentTypes method
    /// </summary>
    public class GetResidentDocumentTypesParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
    }
        
    #endregion Residents & Resident Info

    #region Doctors & Treatment
    /// <summary>
    /// Parameters for GetDoctorVisits request
    /// </summary>
    public class GetDoctorVisitsParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to comma separated ID List of all the residents you wish to retrieve the DoctorVisits for. (E.g. "1203,586,889775")        
        /// </summary>
        public string ResidentIDList;
        /// <summary>
        /// Filter: set to a Date to retrieve all DoctorVisits after set date. 
        /// Leave NULL to retrieve all ResidentParams
        /// </summary>
        public DateTime? DateFrom;
        /// <summary>
        /// Filter: set to a Date to retrieve all DoctorVisits up until set date. 
        /// Leave NULL to retrieve all ResidentParams
        /// </summary>
        public DateTime? DateUntil;
    }
    /// <summary>
    /// Parameters for InsertDoctorVisits request
    /// </summary>
    public class InsertDoctorVisitParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to resident ID of the resident that you want to book the visit for.
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// Set to Date of visit.
        /// </summary>
        public DateTime VisitDate;
        /// <summary>
        /// Set to the Nomenclature ID that is associated with the action performed during the doctor’s visit. (E.g. regular consultation, etc...) This is will determine 
        /// the rebate's paid by the resident's mutuality. (If Applicable.)
        /// </summary>
        public int NomenClatureID;
        /// <summary>
        /// Rate charged for this visit.
        /// </summary>
        public double Rate;
        /// <summary>
        /// Set to optional remarks you want to register for this visit.
        /// </summary>
        public string Remarks;
        /// <summary>
        /// IF ID is passed, the request tries to update existing visit. (E.g. if visit was previously inserted with a wrong rate, you can resubmit the request with the correct rate
        /// and the ID of the existing DoctorVisit. The web service will update the visit according to the parameters set in your new request. The ID of a new doctorvisit is returned
        /// as a IDValue object after a successful insert. Or you can use GetDoctorVisits request to find the visit.
        /// </summary>
        public int? ID;
    }
    /// <summary>
    /// Parameters for RemoveDoctorVisits request
    /// </summary>
    public class RemoveDoctorVisitsParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to ID of the DoctorVisit object you want to remove. The ID of a new doctorvisit is returned
        /// as an IDValue object after a successful insert. Or you can use GetDoctorVisits request to find the visit.
        /// </summary>
        public int? ID;
    }
    /// <summary>
    /// Parameters for GetNomenclature request
    /// </summary>
    public class GetNomenclatureParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: If you wish to retrieve info for a known NomenclatureID you can set it using this property. 
        /// If found, only a Nomenclature object with this ID will be returned.
        /// If left NULL, all Nomenclature objects will be returned.
        /// </summary>
        public int? NomenclatureID;
        /// <summary>
        /// Filter: set to a Date to retrieve all Nomenclature objects valid after set date. 
        /// Leave NULL to retrieve all Nomenclature objects
        /// </summary>
        public DateTime? DateFrom;
        /// <summary>
        /// Filter: set to a Date to retrieve all Nomenclature objects valid up until set date. 
        /// Leave NULL to retrieve all Nomenclature objects
        /// </summary>
        public DateTime? DateUntil;
        /// <summary>
        /// Filter: set to 0 to retrieve all Nomenclature objects, set to 1 to receive only doctor's Nomenclature objects, set to 2 to receive only
        /// Kine nomenclature objects.
        /// </summary>
        public short FilterType;//0:alles, 1: doctor, 2 kine
    }
    #endregion Doctors & Treatment

    #region Registrations, Parameters and Observations

    /// <summary>
    /// Parameters for GetParamList request
    /// </summary>
    public class GetParamListParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
    }

    /// <summary>
    /// Parameters for GetParamDetail request
    /// </summary>
    public class GetParamDetailParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: if you only want to have the possible ParamDetail values for a certain parameter, set this property to the requested Parameter's ID.
        /// Leave NULL to retrieve the ParamDetails for all known Parameter types.
        /// </summary>
        public int? ParamListID;
    }

    /// <summary>
    /// Parameters for GetActionList request
    /// </summary>
    public class GetActionListParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
    }

    /// <summary>
    /// Parameters for InsertParam request
    /// </summary>
    public class InsertParamParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to the ID of the Parameter you are signing/registering. (See ParamList class)
        /// </summary>
        public byte ParamListID;
        /// <summary>
        /// Set to the ID of the resident you are registering the parameter value for.
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// Set to the measured parameter's value. (See ParamList object for reference.)
        ///
        /// A bit more info on blood pressure: There are 2 ways to register the blood pressure parameter.
        /// 
        /// First option -> You can register Systolic (higher number, paramlist ID 2) the Diastolic value (lower number, paramlist ID 3)separately 
        /// using 2 separate requests.
        /// 
        /// Second option -> You can register the blood pressure using ParamList ID 13 and encode both Systolic and Diastolic values in a single digit number.
        /// This value is made up of the combination of both Systolic and Diastolic (padded with zero's) pressure values. 
        /// Let's say you need to register 110/55, in that case you set the Value to 110055. In case you need to register 95/55 you set the Value to 95055.            
        /// </summary> 
        public decimal Value;
        /// <summary>
        /// If applicable, you can set the ID of the ParamDetail value that was measured for this Parameter. (E.g. when registering a pulse of 55 as 'Weak',
        /// set Value2 to 9 -> See ParamDetails)
        /// </summary>
        public byte? Value2;
        /// <summary>
        /// Optional remarks you wish to enter for this Parameter registration.
        /// </summary>
        public string Remarks;
        /// <summary>
        /// Set to CareDate when the parameter was registered.
        /// </summary>
        public DateTime Stamp;
        /// <summary>
        /// If you wish to create an observation (see Observation class) for the Parameter registration, set to True. This way the remarks you entered will
        /// be visible for the users in the CS Software. If no remarks were entered, set to False.
        /// </summary>
        public bool Observation;
        /// <summary>
        /// If you wish to display your remarks/observation in the Care Home's diary, set to True. If no remarks were entered, or you do not wish to show the
        /// entered remarks in the Care Home’s diary. Set to False.
        /// </summary>
        public bool InDiary;
    }

    /// <summary>
    /// Parameters for CreateObservation request
    /// </summary>
    public class CreateObservationParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to the Resident ID you want to create the observation for.
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// Set to the CareDate you wish to create the observation on. (Mostly current day's Care Date.)
        /// </summary>
        public DateTime CareDate;
        /// <summary>
        /// Set to the ID of the CareMoment you wish to register this Observation for. (Morning shift, afternoon,... See Caremoments class en GetCareMoments method.)
        /// </summary>
        public byte? CareMomentID;
        /// <summary>
        /// Observation text.
        /// </summary>
        public string Remarks;
        /// <summary>
        /// You can link you observation to all the different modules in our software. If you link an observation to a CS Software module, the observation will be made
        /// visible to the users of that module. (See GetModlinkables method)
        /// </summary>
        public string LinkToModules;
        /// <summary>
        /// If you wish to display your observation in the Care Home's diary, set to True. If no remarks were entered, or you do not wish to show the
        /// entered remarks in the Care Home's diary. Set to False.
        /// </summary>
        public Boolean Diary;
        /// <summary>
        /// If the observation needs is tied to a certain action. (E.g. washing a senior, tending a wound,...) you can enter that action's ActionListID property here.
        /// If set, the Software can link the observation with set action.
        /// </summary>
        public short? ActionListID;
        /// <summary>
        /// If this action is a planned action from the resident's care plan, you can set this property to that planned item's LinkID here. This connects the observation 
        /// to the care plan item the user was executing.
        /// </summary>
        public int? LinkTaskID;
    }

    /// <summary>
    /// Parameters for CreateRegistration request
    /// </summary>
    public class CreateRegistrationParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to the Resident ID you want to create the registration for.
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// Timestamp when the action is registered. (Will internally be converted to the correct caremoment and care date.) Only use this property
        /// if you don not set the CareDate and CareMomentID properties. In that case, Stamp should be NULL
        /// </summary>
        public DateTime? Stamp;
        /// <summary>
        /// CareDate when the action is registered. Only use this property if you don not set the Stamp property. In that case, CareDate and CareMomentID
        /// should be NULL
        /// </summary>
        public DateTime? CareDate;
        /// <summary>
        /// CareMomentID of the CareMoment you want to register the action for. Only use this property if you don not set the Stamp property. In that case, CareDate and CareMomentID
        /// should be NULL
        /// </summary>
        public byte? CareMomentID;
        /// <summary>
        /// ActionList ID of the action you want to register as executed. (E.g. when you want to register action 'Help clothing resident' set this to its ActionList ID: 4)
        /// </summary>
        public short? ActionListID;
    }

    #endregion Registratios, Parameters and Observations

    #region Care Planning

    /// <summary>
    /// Parameters for SignPlannedTask request.
    /// </summary>
    public class SignPlannedTaskParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to ID of the Resident you are signing the planned task for.
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// Set to Care Date you are signing the planned task on. (See GetCarePlanForResident method.)
        /// </summary>
        public String CareDate;
        /// <summary>
        /// Set to Care Moment ID you are signing the planned task for.  (See GetCarePlanForResident method.)
        /// </summary>
        public int CareMomentID;
        /// <summary>
        /// Set to ActionListID of the planned task you are registering.
        /// </summary>
        public int ActionListID;
        /// <summary>
        /// Set to LinkID of the planned task you are registering. Used in the CS software to link a registered task to the care planning of a resident.
        /// Used in CS software to determine which planned tasks were executed when, by who and which tasks were not executed.
        /// </summary>
        public int Link;
        /// <summary>
        /// ModuleID of the module where the task originated from. currently not used in WZD. Only used to sign taks that were planned by third
        /// party applicaitons. This moduleID is used to determine to which thrid party server we need to relay the signature.
        /// </summary>
        public int? ModuleID;
    }
    /// <summary>
    /// Parameters for SignPlannedTaskNotDone request.
    /// </summary>
    public class SignPlannedTaskNotDoneParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;

        /// <summary>
        /// Set to ID of the Resident you are signing the planned task as NOT DONE for.
        /// </summary>       
        public int ResidentID;

        /// <summary>
        /// Set to Care Date you are signing the planned as NOT DONE for. (See GetCarePlanForResident method.) 
        /// </summary>
        public String CareDate;
        /// <summary>
        /// Set to Care Moment ID you are signing the planned task as NOT DONE for.  (See GetCarePlanForResident method.)
        /// </summary>
        public int CareMomentID;
        /// <summary>
        /// Set to ActionListID of the planned task you are registering as NOT DONE.
        /// </summary>
        public int ActionListID;
        /// <summary>
        /// Set to LinkID of the planned task you are registering. Used in the CS software to link a registered task to the care planning of a resident.
        /// Used in CS software to determine which planned tasks were executed when, by who and which tasks were not executed.
        /// </summary>
        public int Link;
        /// <summary>
        /// ModuleID of the module where the task originated from. currently not used in WZD. Only used to sign taks that were planned by third
        /// party applications. This moduleID is used to determine to which thrid party server we need to relay the signature.
        /// </summary>
        public int? ModuleID;
    }
    /// <summary>
    /// Parameters for GetCaremoments request.
    /// </summary>
    public class GetCareMomentsParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
    }
    /// <summary>
    /// Parameters for GetCarePlanForResident request. 
    /// </summary>
    public class GetCarePlanForResidentParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to a comma separated list of resident ID's you want to receive the CarePlan items for.
        /// </summary>
        public string ResidentID;
        /// <summary>
        /// Set to the CareDate you want to retrieve the CarePlan items for.
        /// </summary>
        public DateTime CareDate;
        /// <summary>
        /// Set to a comma separated list of all the CareMoment ID's you want to retrieve the CarePlan items for.
        /// </summary>
        public string CareMomentID;
    }
    /// <summary>
    /// Parameter for the SignPlannedParam
    /// </summary>
    public class SignPlannedParamParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to the ParamListID of the parameters you are registering. (See GetCarePlanForResident method and CarePlanItem class)
        /// </summary>
        public byte? ParamListID;
        /// <summary>
        /// Set to ID of the Resident you are signing the planned parameter for.
        /// </summary>       
        public int? ResidentID;
        /// <summary>
        /// Set to Care Moment ID you are signing the planned parameter for.  (See GetCarePlanForResident method.)
        /// </summary>
        public byte? CareMomentID;
        /// <summary>
        /// Set to Care Date you are signing the planned the planned parameter for. (See GetCarePlanForResident method.) 
        /// </summary>
        public DateTime? CareDate;
        /// <summary>
        /// Set to measured parameter value you want to register for this planned parameter. (See ParamList object for reference.)
        ///
        /// A bit more info on blood pressure: There are 2 ways to register the blood pressure parameter.
        /// 
        /// First option -> You can register Systolic (higher number, paramlist ID 2) the Diastolic value (lower number, paramlist ID 3) separately 
        /// using 2 separate requests.
        /// 
        /// Second option -> You can register the blood pressure using ParamList ID 13 and encode both Systolic and Diastolic values in a single digit number.
        /// This value is made up of the combination of both Systolic and Diastolic (padded with zero's) pressure values. 
        /// Let's say you need to register 110/55, in that case you set the Value to 110055. In case you need to register 95/55 you set the Value to 95055.            
        /// </summary>
        public decimal? Value;
        /// <summary>
        /// If applicable, you can set the ID of the ParamDetail value that was measured for this Parameter. (E.g. when registering a pulse of 55 as 'Weak',
        /// set Value2 to 9 -> See ParamDetails)
        /// </summary>
        public byte? Value2;
        /// <summary>
        /// Set to true if you want to create an observation for this planned parameter. Make sure you set the observation's text in the Remarks property
        /// Else set to false and set Remarks to null.
        /// </summary>
        public bool? Observation;
        /// <summary>
        /// Observation's text if you choose to create an observation. (Make sure Observation property is set to true.)
        /// </summary>
        public string Remarks;
        /// <summary>
        /// Set to true if you want the created observation to appear in the Care Home's Diary. (Only set to true when Observation property is set to true and
        /// remarks property is not null.
        /// </summary>
        public bool? InDiary;
        /// <summary>
        /// Set to LinkID of the planned parameter you are registering. Used in the CS software to link a registered task to the care planning of a resident.
        /// Used in CS software to determine which planned tasks were executed when, by who and which tasks were not executed. (See CarePlanItem and GetCarePlanForResident request.)
        /// </summary>
        public int? Link;
    }
    #endregion Care Planning

    #region Communication
    /// <summary>
    /// Parameters for GetAppointments request.
    /// </summary>
    public class GetAppointmentsParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: If you want to get the appointments for a specific resident, set this parameter property to the Resident's ID.
        /// Leave null to retrieve the appointments for all residents.
        /// </summary>
        public int? SeniorID;
        /// <summary>
        /// Set to a Date to retrieve all Appointments after set date. 
        /// </summary>
        public DateTime DateFrom;
        /// <summary>
        /// Set to a Date to retrieve all Appointments up until set date. 
        /// </summary>
        public DateTime DateUntil;
    }
    /// <summary>
    /// Parameters for GetAppointmentTypes request.
    /// </summary>
    public class GetAppointmentTypesParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
    }
    /// <summary>
    /// Parameters for GetCommunicationItems request
    /// </summary>
    public class GetCommunicationItemsParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to the ID of the module ID you want to get the communication items for. (See GetModules request and Module class)
        /// </summary>
        public int ModuleID;
        /// <summary>
        /// Filter: Set to false to only retrieve unread communication. Set to true to only get read communication. Leave NULL the get both.
        /// </summary>
        public short? Read;
        /// <summary>
        /// Filter: Set to a Resident's ID to only retrieve communication items for this resident. Leave NULL to receive communication items
        /// for all residents.
        /// </summary>
        public int? ResidentID;
    }
    /// <summary>
    /// Parameters for GetDiary request.
    /// </summary>
    public class GetDiaryParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: Set to a resident ID if you only want to get Diary items for set resident. Leave NULL to retrieve Diary items for all Residents
        /// </summary>
        public int? ResidentID;
        /// <summary>
        /// Set to a Date to retrieve all Diary Items after set date. 
        /// </summary>
        public DateTime DateFrom;
        /// <summary>
        /// Set to a Date to retrieve all Diary Items up until set date. 
        /// </summary>
        public DateTime DateUntil;
    }
    /// <summary>
    /// Parameters for GetDiary_v2 request
    /// </summary>
    public class GetDiaryParameters_v2
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: Set to a resident ID if you only want to get Diary items for set resident. Leave NULL to retrieve Diary items for all Residents
        /// </summary>
        public int? ResidentID;
        /// <summary>
        /// Filter: Set to a Department ID if you only want to get Diary items for set Department. Leave NULL to retrieve Diary items for all Departments
        /// </summary>
        public int? DepartmentID;
        /// <summary>
        /// Filter: set to a Date to retrieve all Diary Items after set date. 
        /// /// </summary>
        public DateTime DateFrom;
        /// <summary>
        /// Filter: set to a Date to retrieve all Diary Items after set date. 
        /// </summary>
        public DateTime DateUntil;
        /// <summary>
        /// Set to true if you want to receive diary items related to residents. (See Diary_v2 class)
        /// Set to false to exclude diary items related to residents from result list.
        /// </summary>
        public bool GetResidentDiary;
        /// <summary>
        /// Set to true if you want to receive general diary items, not related to residents. (See Diary_v2 class)
        /// Set to false to exclude general diary items, not related to residents from result list.
        /// </summary>
        public bool GetGeneralDiary;
    }
    /// <summary>
    /// Parameters for GetModules request
    /// </summary>
    public class GetModulesParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
    }
    /// <summary>
    /// Parameters for CreateAppointment request
    /// </summary>
    public class CreateAppointmentParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to the Resident ID you want to add the appointment for. Appointment will be visible in the Resident's calendar in CS Software.
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// Set to the ID of the AppointmentType of your appointment. (See AppointmentType class and GetAppointmentTypes method)
        /// </summary>
        public int AppointmentTypeID;
        /// <summary>
        /// Set to Appointment's start time
        /// </summary>
        public DateTime StartTime;
        /// <summary>
        /// Set to Appointment's end time
        /// </summary>
        public DateTime EndTime;
        /// <summary>
        /// Set to any comments/details you want to enter for this appointment.
        /// </summary>
        public string Comment;
        /// <summary>
        /// Set to true if you want this appointment to be shown in the Care Home's diary. Else set to false.
        /// </summary>
        public Boolean ItemInDiary;
        /// <summary>
        /// Set to true if you want this appointment to be shown (as an information item) in this resident's careplan. (See GetCarePlanForResident request and CarePlanItem class)
        /// If you choose to this, an task will be generated for this appointment. But the task is created as an informational item, and can not be signed as executed.
        /// 
        /// Else set to false/
        /// </summary>
        public Boolean ItemOnPlanng;
    }
    /// <summary>
    /// Parameters for MarkCommunicationAsRead
    /// </summary>
    public class MarkCommunicationAsReadParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to the ID of the Communication item you want to mark as read/unread
        /// </summary>
        public int CommunicationID;
        /// <summary>
        /// Set to true if you want to mark the Communication item as read, set to false if you want to mark the communication item
        /// as unread.
        /// </summary>
        public bool Read;
    }
    /// <summary>
    /// Parameters for MarkAllCommunicationAsRead
    /// </summary>
    public class MarkAllCommunicationAsReadParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to true if you want to mark the all the Communication item as read, set to false if you want to mark all the communication item
        /// as unread.
        /// </summary>
        public bool Read;
        /// <summary>
        /// Filter: if you only want to mark a certain resident’s communication items read for you session set this property to the resident's ID.
        /// Else leave NULL
        /// </summary>
        public int? ResidentID; //Leave null to mark for ALL residents
        /// <summary>
        /// Filter: Set this to mark all Communication items up to a certain Communication ID as read/unread. All newer items will not be changed.
        /// This is convenient if you want to mark all items up to a certain point as unread. (E.g. when marking a backlog as read.)
        /// Leave NULL to change all communication items.
        /// </summary>
        public int? MaxCommunicationID; //Leave null to mark ALL communication as read/unread
    }
    /// <summary>
    /// Parameters for RespondToCommunication request
    /// </summary>
    public class RespondToCommunicationParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to ID of the Communication Item you are responding to.
        /// Leave NULL to create a new string of communication. This item being the first in the message string.
        /// </summary>
        public int? CommunicationID;
        /// <summary>
        /// Set to Timestamp this communication item was sent. Leave NULL to create with current date/time/
        /// </summary>
        public DateTime? Stamp;
        /// <summary>
        /// Text of the communication
        /// </summary>
        public string Remarks;
        /// <summary>
        /// Set to a comma separated list of all modules (modlinkables) you want your communication item to be visible in. (See GetModuleList and GetModlinkables requests)
        /// </summary>
        public string LinkToModules;
        /// <summary>
        /// Set to true if you want your communication to be displayed in the Care Home's diary. Else set to False.
        /// </summary>
        public bool? Diary;
    }
    /// <summary>
    /// Parameters for GetModlinkables request.
    /// </summary>
    public class GetModLinkablesParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to comma separated ID List of all the residents you wish to retrieve the modlinkabes for. (E.g. "1203,586,889775")
        /// </summary>
        public string ResidentID;
        /// <summary>
        /// Set to true if you only want to retrieve resident specific modlinkables. (E.g. Modlinkable ID for a resident's wound, a resident's follow-up item,...)
        /// Set to false if you want the general modules to be included in the list.
        /// </summary>
        public Boolean ResidentSpecificOnly;
    }
    #endregion Communication

    #region Call System
    /// <summary>
    /// Parameters for GetCallByID request
    /// </summary>
    public class GetCallByIDParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to External ID property of the call you want to retrieve. (See Call class)
        /// </summary>
        public string CallID;
    }
    /// <summary>
    /// Parameters for GetCallForeResident request
    /// </summary>
    public class GetCallsForResidentParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: Set to resident ID if you only want to retrieve calls for set resident. 
        /// Leave null to retrieve the calls for all residents.
        /// </summary>
        public int? ResidentID;
        /// <summary>
        /// Set to a Date to retrieve all Calls after set date. 
        /// </summary>
        public DateTime DateFrom;
        /// <summary>
        /// Set to a Date to retrieve all Calls up until set date. 
        /// </summary>
        public DateTime DateUntil;
    }
    /// <summary>
    /// Parameters for GetCallsForUser request
    /// </summary>
    public class GetCallsForUserParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// User ID of the user you wish to retrieve the Calls for. Looks for the UserID property of the call object and returns only matching ones.
        /// Can be left NULL to automatically retrieve the calls for you session's user ID.
        /// </summary>
        public int? UserID;        
        /// <summary>
        /// Set to a Date to retrieve all Calls after set date. 
        /// </summary>        
        public DateTime DateFrom;
        /// <summary>
        /// Set to a Date to retrieve all Calls up until set date. 
        /// </summary>
        public DateTime DateUntil;
        /// <summary>
        /// Set to true to include closed calls in the result list. (Calls that have a StopDate set and are considered answered/handled)
        /// Set to false to omit closed the from the result list.
        /// </summary>
        public bool GetClosedCalls;
        /// <summary>
        /// Set to true to include open calls in the result list. (Calls that have no StopDate set and are considered unanswered/open)
        /// Set to false to omit open calls from the result list.
        /// </summary>
        public bool GetOpenCalls;
    }
    /// <summary>
    /// Parameters for EditCallForResidentParameters
    /// </summary>
    public class EditCallForResidentParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to your reference. Will be stored in the ExternalID property of the created call. Used to update the call object afterwards.
        /// If a call with this External ID already exists in the database, the existing call object will be updated with the non NULL parameters of this request.
        /// 
        /// If the ID is not found, a new Call will be created. Keep in mind that ID, RoomDesc and Start properties are required to create the initial call. Not required when updating the call.
        /// </summary>
        public string CallID;
        /// <summary>
        /// Start DateTime of the call. Leave NULL if you don not want this property to be updated in the database. Required when call is initially created.
        /// </summary>
        public DateTime? Start;
        /// <summary>
        /// DateTime when call was answered and someone was present in the room. Leave NULL if you don not want this property to be updated in the database.
        /// </summary>
        public DateTime? Presence;
        /// <summary>
        /// DateTime when the call was closed. Leave NULL if you do not want this property to be updated in the database.
        /// </summary>
        public DateTime? Stop;
        /// <summary>
        /// RoomDesc property of the Room object you want to create the call for. Leave NULL if you do not want this property to be updated in the database.
        /// </summary>
        public string RoomDesc;
        /// <summary>
        /// Set to define priority of the call. Mostly only used by CS. This will display linked text in the Application. Required when initially creating the call.
        /// Leave NULL if you do not want this property to be updated in the database.
        /// </summary>
        public byte? Priority;
        /// <summary>
        /// Set to User ID that responded to the call. Leave NULL if you do not want this property to be updated in the database.
        /// </summary>
        public int? UserID;
        /// <summary>
        /// Can  be used to set remarks/text for the call. Leave NULL if you do not want this property to be updated in the database.
        /// </summary>
        public string Description;
        /// <summary>
        /// Set to Resident ID the call was registered for. If null, the resident ID will be inferred from the given RoomDesc property.
        /// Leave NULL if you don not want this property to be updated in the database.
        /// </summary>
        public int? ResidentID;
        /// <summary>
        /// Set to a CallReasonID (See GetCallReasosns method) to specifiy the nature/reason of the nursecall.
        /// Leave NULL if you don not want this property to be updated in the database.
        /// </summary>
        public int? CallReasonID;
    }
    /// <summary>
    /// Parameters for GetCallReasons
    /// </summary>
    public class GetCallReasonsParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Use to to filter active reasons. Set to true to retrieve only the active CalLReasons, set to false to retrieve the inactive CallReasons set to NULL to retrieve both.
        /// </summary>
        public bool? Active;
    }
    #endregion Call System

    #region Users
    /// <summary>
    /// Parameters for GetUserForUserKey request.
    /// </summary>
    public class GetUserForUserKeyParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Users iButton property as configured in CS software's user management. This is the external ID with which the user can be referenced. (E.g. Badge number, iButton code,...)
        /// </summary>
        public string UserKey;
    }
    /// <summary>
    /// Parameters for GetUserList request.
    /// </summary>
    public class GetUSerListParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: Set to true to only retrieve active users. Set to False to retrieve only inactive users. Leave NULL to retrieve both.
        /// </summary>
        public bool? Active;
        /// <summary>
        /// Filter: set to a User ID to only retrieve the User Object of set user. Leave NULL to retrieve all users.
        /// </summary>
        public int? UserID;
    }
    /// <summary>
    /// Parameters for ValdidateUserByEmail request.
    /// </summary>
    public class ValidateUserByEmailParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string sessionid;
        /// <summary>
        /// email address of the user you would like to validate. (As entered in the CS Software's user management.)
        /// </summary>
        public string userEmail;
        /// <summary>
        /// Set to current User's password.
        /// </summary>
        public string userPassword;
        /// <summary>
        /// Set to the ID of the WZDatabase you wish to validate this user for. The request will search for a user with matching
        /// password and email address in this database. (See WZDatabase class and GetWZDatabases request.)
        /// </summary>
        public short? databaseID;
    }
    /// <summary>
    /// Parameters for ValidateUserByUserName request.
    /// </summary>
    public class ValidateUserByUserNameParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string sessionid;
        /// <summary>
        /// User name/initials of the user you would like to validate. (As entered in the CS Software's user management.)
        /// </summary>
        public string userName;
        /// <summary>
        /// Set to current User's password.
        /// </summary>
        public string userPassword;
        /// <summary>
        /// Set to the ID of the WZDatabase you wish to validate this user for. The request will search for a user with matching
        /// username/initials and password address in this database. (See WZDatabase class and GetWZDatabases request.)
        /// </summary>
        public short? databaseID;
    }
    #endregion Users

    #region Medication
    /// <summary>
    /// Parameters for GetMedicationPlanForResident
    /// </summary>
    public class GetMedicationPlanForResidentParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to date you want to retrieve the planning for.
        /// </summary>
        public DateTime MedicationDate;
        /// <summary>
        /// Set to a comma separated list of Medication Moment ID's you want to retrieve the Medication Plan for.
        /// </summary>
        public string MedmomentID;
        /// <summary>
        /// Set to a comma separated list of Resident ID's you want to retrieve the Medication Plan for.
        /// </summary>
        public string ResidentID;
        /// <summary>
        /// Set the desired action type you want to get the planning for.
        /// 3: distribute the medication, 4: administer the medication to the resident.
        /// </summary>
        public Byte ActionType; //3=uitdelen, 4= toedienen
    }
    /// <summary>
    /// Parameters for GetMedicationSchemeForResident request.
    /// </summary>
    public class GetMedicationSchemeForResidentParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to a comma separated list of Resident ID's you want to retrieve the Medication Plan for. 
        /// </summary>
        public string ResidentID;
        /// <summary>
        /// Set to true if you want to retrieve the inactive medication items as well.
        /// Set to false if you only want to retrieve the currently active medication items in the medication schedule/scheme.
        /// </summary>
        public Boolean ShowHistory;
    }
    /// <summary>
    /// Parameters for GetMedicationAdminforms request
    /// </summary>
    public class GetMedicationAdminFormsParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
    }
    /// <summary>
    /// Parameters for GetMedicationMoments request
    /// </summary>
    public class GetMedicationMomentsParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
    }
    /// <summary>
    /// Parameters for GetMedicationDeptAccess Request
    /// </summary>
    public class GetMedicationDeptAccessParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
    }
    /// <summary>
    /// Parameters for SignMedicationPlanItem request.
    /// </summary>
    public class SignMedicationPlanItemParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to the Resident ID you are signing the planned medication for.
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// Set to the date you are signing the planned medication for.
        /// </summary>
        public DateTime MedicationDate;
        /// <summary>
        /// Set to the ID of the medicationmoment you are signing the medication item for. (See GetMedicationPlanForResident.
        /// </summary>
        public byte MedmomentID;
        /// <summary>
        /// Set to 3 if you want to sign the distributing of medication.
        /// Set to 4 if you want to sign the administering of medication. (Note, if you are signing the administering the medication.
        /// the system expects that you already distributed the medication. So in practice, we require a sign request for both actions.
        /// In theory you can not administer the medication without distributing it.) So in most case, a SignMedicationPlanItem request with ActionType 3 is sent, 
        /// followed by a SignMedicationPlanItem Request with ActionType 4
        /// </summary>
        public byte ActionType;
        /// <summary>
        /// Set to the MedicationID of the MedicationPlan item your are signing. (Defines which medicine is distributed, administered.)
        /// See GetMedicationPlanForResident.
        /// </summary>
        public int MedicationID;
        /// <summary>
        /// Set to Dosage (if applicable) that was administered. Ex. if half a tablet was given to the resident, set this to 0,5
        /// </summary>
        public decimal Quantity;
    }
    /// <summary>
    /// Parameters for SignMedicationPlanItemNotDone request.
    /// </summary>
    public class SignMedicationPlanItemNotDoneParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to the Resident ID you are signing the planned medication as NOT DONE for.
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// Set to the date you are signing the planned medication as NOT DONE for.
        /// </summary>
        public DateTime MedicationDate;
        /// <summary>
        /// Set to the MedicationID of the MedicationPlan item you are signing as NOT DONE. (Defines which medicine is distributed, administered.)
        /// See GetMedicationPlanForResident.
        /// </summary>
        public int MedicationID;
        /// <summary>
        /// Set to the MedicationID of the MedicationPlan item you are signing as NOT DONE. (Defines which medicine is distributed, administered.)
        /// See GetMedicationPlanForResident.
        /// </summary>
        public byte ActionType;
        /// <summary>
        /// Can be used to enter text describing why the MecicationPlanItem was signed as NOT DONE.
        /// </summary>
        public string Remarks;
    }
    /// <summary>
    /// Parameters for GetVaccinationType
    /// </summary>
    public class GetVaccinationTypeParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: set to true if you only want to retrieve the active vaccinationtypes. Set to False if you only want to retrieve the inactive
        /// vaccinationtypes. Set to NULL if you want to retrieve both.
        /// </summary>
        public bool? Active;
    }
    /// <summary>
    /// Parameters for GetMedicationStandingOrderLog method
    /// </summary>
    public class GetMedicationStandingOrderLogParams
    {
        /// <summary>
        ///  User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to a Date to retrieve all ResidentParams after set date. 
        /// </summary>
        public DateTime DateFrom;
        /// <summary>
        /// Set to a Date to retrieve all ResidentParams until set date. 
        /// </summary>
        public DateTime DateUntil;
        /// <summary>
        /// Filter: set to a departmentID to only retrieve StandingOrderLogs for set department. Leave NULL to retrieve
        /// for all departments.
        /// </summary>
        public int? DepartmentID;
        /// <summary>
        /// Set to comma separated ID List of all the residents you wish to retrieve the StandingOrderLog info for. (E.g. "1203,586,889775")
        /// </summary>
        public string ResidentIDList;
    }
    /// <summary>
    /// Parameters for SignMedicationStandingOrder method.
    /// </summary>
    public class SignMedicationStandingOrderParams
    {
        /// <summary>
        ///  User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Unique ID of the resident you want to sign the medication for.
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// Set to Timestamp when the medication was given.
        /// </summary>
        public DateTime MedicationGivenOn;
        /// <summary>
        /// Set to the treatment ID of the standing order as it appears on the resident's medicationscheme.
        /// You can also use MedicationID property to sign the order. When doing so, leave TreatmentID property null.
        /// </summary>
        public int? TreatmentID; //Kan zowel via medicationID als treatmentID afgetekend worden.
        /// <summary>
        /// Set to the MedicationID of the standing order as it appears on the resident's medicationscheme.
        /// You can also use TreatmentID property to sign the order. When doing so, leave MedicationID  property null.
        /// </summary>
        public int? MedicationID; //Kan zowel via medicationID als treatmentID afgetekend worden.
        /// <summary>
        /// Set to any remarks you want to register while signing the order.
        /// </summary>
        public string Remarks;
        /// <summary>
        /// Set to quantity of units that was given. (0.5 tablets, 2 tablets,...)
        /// </summary>
        public decimal? Quantity;
    }
    /// <summary>
    /// Parameters for ValidateMedicationSchemeForResident method.
    /// </summary>
    public class ValidateMedicationSchemeForResidentParams //Method to mark a Medication Scheme as validated. Must be called using a Doctor's user session.
    {
        /// <summary>
        ///  User's SessionID received after successful Login.
        /// </summary>
        public string SessionID; //SessionID
        /// <summary>
        /// Unique ID of the resident you want to validate the medication scheme for.
        /// </summary>
        public int ResidentID; //Resident ID
        /// <summary>
        /// Set to timestamp of validation. Leave NULL to use the current server time.
        /// </summary>
        public DateTime? TimeStamp; //Null will use current server time
    }
    #endregion Medication

    #region Wounds
    /// <summary>
    /// Parameters for GetWoundCategories request.
    /// </summary>
    public class GetWoundCategoriesParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: Set to True if you only want to retrieve the current active wound categories. Set to False if you only want to retrieve the inactive wound categories.
        /// Leave NULL to retrieve both.
        /// </summary>
        public bool? Active;
    }
    /// <summary>
    /// Parameters for GetWoundOrigins request.
    /// </summary>
    public class GetWoundOriginsParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
    }
    /// <summary>
    /// Parameters for GetWoundClassificationFields request.
    /// </summary>
    public class GetWoundClassificationFieldsParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Comma separated list of field names of the WoundClassificationFields for which you want to get the type/list of values 
        /// </summary>
        public string FieldNameList;
    }
    /// <summary>
    /// Parameters for GetWounds request
    /// </summary>
    public class GetWoundsParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to comma separated ID List of all the residents you wish to retrieve the wounds for. (Eg "1203,586,889775") 
        /// </summary>
        public string ResidentID; //CSV of residentID's
        /// <summary>
        /// Filter: set to 'True' to only receive a list of active wounds. Set to 'False' to receive inactive wounds.
        /// </summary>
        public bool Active;
        /// <summary>
        /// Filter: set to 'True' to receive the fill woundclassification history for each resident wound. Set to 'False' to only retrieve the
        /// last known (and thus current) classification for each resident's wound.
        /// </summary>
        public bool ClassificationHistory;
    }
    /// <summary>
    /// Parameters for InsertResidentWound request.
    /// </summary>
    public class InsertResidentWoundParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to Resident ID of resident for which you want to register the wound.
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// Set to Woundlocation ID of the location of the wound. (At the moment this list is hardcoded. Might be made into a request in future release)
        /// 1;lende rechts;échine dorsale droite
        /// 2;linkerenkel binnenkant;intérieur cheville gauche
        /// 3;linkerbil;fesse gauche
        /// 4;linkerdij binnenkant;intérieur cuisse gauche
        /// 5;rechteronderbeen binnenkant;intérieur partie inférieure jambe droite
        /// 6;rechteronderarm;avant-bras droit
        /// 7;linkermiddelteen;orteil du milieu pied gauche
        /// 8;borstkas linksboven;dessus poitrine gauche
        /// 9;linkervoet bovenkant;dessus pied gauche
        /// 10;linkeronderarm;avant-bras gauche
        /// 11;rechter kleine teen;petit orteil pied droit
        /// 12;rechterenkel binnenkant;intérieur cheville droite
        /// 13;linkerdij achterkant;cuisse gauche postérieure
        /// 14;rechterdij buitenkant;extérieur cuisse droite
        /// 15;linkerenkel buitenkant;extérieur cheville gauche
        /// 16;rechterschouder;épaule droite
        /// 17;rechterwang;joue droite
        /// 18;linkerpols;poignet gauche
        /// 19;rechtervoet teen 4;orteil 4 pied droit
        /// 20;bovenrug;haut du dos
        /// 21;navel;nombril
        /// 22;linkerschouder;épaule gauche
        /// 23;linkerborst onderkant;dessous sein gauche
        /// 24;rechtermiddelvinger;majeur main droite
        /// 25;linkerwijsvinger;index main gauche
        /// 26;linkerdij voorkant;cuisse gauche antérieure
        /// 27;borstkas rechtsonder;dessous poitrine droite
        /// 28;rechtervoet binnenkant;intérieur pied droit
        /// 29;voorhoofd;front
        /// 30;borstkas boven;dessus poitrine
        /// 31;rechter grote teen;gros orteil pied droit
        /// 32;linkerkaak;mâchoire gauche
        /// 33;nek links;cou côté gauche
        /// 34;linkeronderbeen buitenkant;extérieur partie inférieure jambe gauche
        /// 35;rechterwijsvinger;index main droite
        /// 36;rechteronderbeen achterkant;partie inférieure jambe droite postérieure
        /// 37;rechterheup;hanche côté droit
        /// 38;linkerborst bovenkant;dessus sein gauche
        /// 39;rechtertepel;mamelon droit
        /// 40;nek achterkant;nuque
        /// 41;linker kleine teen;petit orteil pied gauche
        /// 42;linkerringvinger;annulaire main gauche
        /// 43;rechterdij achterkant;cuisse droite postérieure
        /// 44;schaamstreek;pubis
        /// 45;rechterringvinger;annulaire droit
        /// 46;linkerhand bovenkant;dessus main gauche
        /// 47;nek rechts;cou côté droit
        /// 48;rechterborst bovenkant;dessus sein droit
        /// 49;rechteroor;oreille droite
        /// 50;rechterborst linkerkant;sein droit côté gauche
        /// 51;rechterbovenarm;partie supérieure bras droit
        /// 52;linkertepel;mamelon gauche
        /// 53;linkerbovenarm;partie supérieure bras gauche
        /// 54;linkerelleboog binnenkant;intérieur coude gauche
        /// 55;linker grote teen;gros orteil pied gauche
        /// 56;linkerknie binnenkant;intérieur genou gauche
        /// 57;borstkas onder;dessous poitrine
        /// 58;linkerpink;auriculaire main gauche
        /// 59;gezicht;visage
        /// 60;rechterborst onderkant;dessous sein droit
        /// 61;rechterelleboog binnenkant;intérieur coude droit
        /// 62;linkerhiel;talon gauche
        /// 63;linkeroor;oreille gauche
        /// 64;linkervoet teen 4;orteil 4 pied gauche
        /// 65;linkerborst linkerkant;sein gauche côté gauche
        /// 66;rechterdij voorkant;cuisse droite antérieure
        /// 67;linkerdij buitenkant;extérieur cuisse gauche
        /// 68;rechterpols;poignet droit
        /// 69;kin;menton
        /// 70;rechterborst rechterkant;sein droit côté droit
        /// 71;linkerpalm;paume main gauche
        /// 72;rechtervoet teen 2;orteil 2 pied droit
        /// 73;rechterenkel;cheville droite
        /// 74;rechterbil;fesse droite
        /// 75;mond;bouche
        /// 76;buik;ventre
        /// 77;onderbuik;abdomen
        /// 78;linkerwang;joue gauche
        /// 79;rechtermiddelteen;orteil du milieu pied droit
        /// 80;linkerschouder;épaule gauche
        /// 81;linker achillespees;tendon d'achille gauche
        /// 82;staartbeentje;coccyx
        /// 83;rechteroog;oeil droit
        /// 84;kruin;couronne
        /// 85;lende links;échine dorsale gauche
        /// 86;onderrug;bas du dos
        /// 87;rechterknie achterkant;genou droit postérieur
        /// 88;linkerelleboog buitenkant;extérieur coude gauche
        /// 89;rechterpalm;paume main droite
        /// 90;linkervoet;pied gauche
        /// 91;neus;nez
        /// 92;rechterduim;pouce main droite
        /// 93;rechter achillespees;tendon d'achille droit
        /// 94;rechterelleboog buitenkant;extérieur coude droite
        /// 95;borstkas rechstboven;dessus poitrine droite
        /// 96;rechterknie binnenkant;intérieur genou droit
        /// 97;rechterhiel;talon droit
        /// 98;rechteronderbeen buitenkant;extérieur partie inférieure jambe droite
        /// 99;linkerknie buitenkant;extérieur genou gauche
        /// 100;rechterknie buitenkant;extérieur genou droit
        /// 101;rechterdij binnenkant;intérieur cuisse droite
        /// 102;linkerknie;genou gauche
        /// 103;rechteronderbeen voorkant;partie inférieure jambe droite antérieure
        /// 104;rechtervoet bovenkant;dessus pied droit
        /// 105;linkeroog;oeil gauche
        /// 106;linkerduim;pouce gauche
        /// 107;linkerenkel;cheville gauche
        /// 108;linkerknie achterkant;genou gauche postérieur
        /// 109;linkeronderbeen voorkant;partie inférieure jambe gauche antérieure
        /// 110;rechterschouder;épaule droite
        /// 111;linkerborst rechterkant;sein gauche côté droit
        /// 112;linkervoet binnenkant;intérieur pied gauche
        /// 113;linkermiddelvinger;majeur main gauche
        /// 114;rechterpink;auriculaire main droite
        /// 115;rechterenkel buitenkant;extérieur cheville droite
        /// 116;linkeronderbeen binnenkant;intérieur partie inférieure jambe gauche
        /// 117;linkerheup;hanche gauche
        /// 118;rechtervoet buitenkant;extérieur pied droit
        /// 119;borstkas linksonder;dessous poitrine gauche
        /// 120;linkeronderbeen achterkant;partie inférieure jambe gauche postérieure
        /// 121;achterhoofd;arrière de la tête
        /// 122;hals;cou
        /// 123;rechtervoet;pied droit
        /// 124;rechterhand bovenkant;dessus main droite
        /// 125;linkervoet buitenkant;extérieur pied gauche
        /// 126;linkervoet teen 2;orteil 2 pied gauche
        /// 127;rechterknie;genou droit
        /// 128;armen;bras
        /// 129;benen;jambes
        /// 130;billen;fesses
        /// 131;borsten;seins
        /// 132;buik;ventre
        /// 133;handen;mains
        /// 134;hoofd;tête
        /// 135;middel;taille
        /// 136;nek;cou
        /// 137;rug;dos
        /// 138;torso;torse
        /// 139;voeten;pieds
        /// 140;kussentje rechtervoetzool;petit coussin plante du pied droit
        /// 141;rechtervoetzool;plante du pied droit
        /// 142;rechterhiel onderkant;dessous du talon droit
        /// 143;kussentje linkervoetzool;petit coussin plante du pied gauche
        /// 144;linkervoetzool;plante du pied gauche
        /// 145;linkerhiel onderkant;dessous du talon gauche
        /// 146;bilspleet;fourchette
        /// 147;schouderblad links;omoplate gauche
        /// 148;schouderblad rechts;omoplate droite
        /// 149;liesplooi links;aine gauche
        /// 150;liesplooi rechts;aine droite
        /// 151;scrotum;scrotum
        /// 152;penis;penis
        /// 153;onder de linkerborst;sous le sein gauche
        /// 154;onder de rechterborst;sous le sein droit
        /// </summary>
        public int WoundLocationID;
        /// <summary>
        /// Set to 'true' if the wound is on the front side of the body, set to 'false' of the wound is on the back side of the body.
        /// </summary>
        public bool FrontOfBody;
        /// <summary>
        /// Set this to the Woundlocation ID's name. But additional, specific details about the location can be further specified here. E.g. location ID: 69 is 'chin'
        /// If registering a wound to the resident's chin. But you wish to specify, the wound is on the left side of the chin you can:
        /// 
        /// Set WoundLocationID to: 69
        /// Set WoundLocationText to: 'Chin - left side'
        /// </summary>
        public string WoundLocationText;
        /// <summary>
        /// X-Location on the CS Software's vector image. (Only used in CS software. See ResidentWound class, frontofbody property.)
        /// </summary>
        public double LocationX;
        /// <summary>
        /// X-Location on the CS Software's vector image. (Only used in CS software. See ResidentWound class, frontofbody property.)
        /// </summary>
        public double LocationY;
        /// <summary>
        /// Set to timestamp of when the wound was first observer.
        /// </summary>
        public DateTime FirstObservedOn;
        /// <summary>
        /// Set to User ID of the user who first observed this wound.
        /// </summary>
        public int? FirsObservedByUser;
        /// <summary>
        /// Set to the Origin ID of the place where the resident was injured. (Care Home, Hospital,...) See GetWoundOrigins request.
        /// </summary>
        public int OriginID;
        /// <summary>
        /// Set to the classification you want the wound to be created with. (See ResidentWoundClassification class) 
        /// </summary>
        public ResidentWoundClassification Classification;
    }
    /// <summary>
    /// Parameters for InsertResidentWoundClassification request.
    /// </summary>
    public class InsertResidentWoundClassificationParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Set to the ID of the wound you want to (re)evaluate/create a classification for.
        /// </summary>
        public int WoundID;
        /// <summary>
        /// Set to the unique ID of the wound you want to re-evaluate, create a classification for.
        /// </summary>
        public int WoundCategoryID;
        /// <summary>
        /// Set to timestamp when this classification was created/registered
        /// </summary>
        public DateTime CreatedOn;
        /// <summary>
        /// Set to User ID who created this classification.
        /// </summary>
        public int CreatedByUserID;
        /// <summary>
        /// Set to any remarks you want to register for this classification. (E.g. "Wound is healing nicely.")
        /// </summary>
        public string Remarks;
        /// <summary>
        /// Set to a list of the ClassificationDetailValues. These make up the the detail for this classification. (E.g. Cut depth, cut length, color of the wound...)
        /// See WoundClassificationDetailValue class, WoundClassificationFieldValue class. (Basically Fieldname/Value pairs) 
        /// </summary>
        public List<ResidentWoundClassificationDetailValue> Values;
    }
    #endregion Wounds

    #region Invoicing
    /// <summary>
    /// Parameters for InsertVendorInvoiceLine request
    /// </summary>
    public class InsertVendorInvoiceLineParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Date on which you want to book the cost/good/service.
        /// If the period this date belongs to hasn't been invoiced yet. This cost/good/service will be invoiced on that month's resident invoice.
        /// If the period has already been invoiced. This cost/good/service will be invoiced in the next uninvoiced invoice period.
        /// 
        /// E.g. Your application's last invoices are created on 30/05/2016 
        /// 
        /// If you book a cost with date 14/04/2016. This cost will be invoiced on the invoices generated at the end of June. (Invoices of June 2016)
        /// 
        /// If you book a cost on 21/07/2016. This cost will be invoiced on the invoices generated at the end of July. (Invoices of July 2016)
        /// </summary>
        public DateTime Date;
        /// <summary>
        /// Unique ID of the resident you want to book the cost for.
        /// </summary>
        public int? ResidentID;
        /// <summary>
        /// InvoiceCodeName of the cost/good/service you want to book
        /// </summary>
        public string InvoiceCodeName;
        /// <summary>
        /// Quantity of this cost/good/service
        /// </summary>
        public short Quantity;
        /// <summary>
        /// Price Per Unit of this cost/good/service. (Total of the invoice line will be calculated in the CS Software.)
        /// </summary>
        public decimal PricePerUnit;

        /// <summary>
        /// Remarks you want to set for this invoice line. (Description of the invoice line which will appear on the invoice.)
        /// 
        /// However, in the CS Software, users have the ability to set 3 different grouping options. 
        /// 1/ No Grouping. Every line inserted will be shown on the invoice, with the code name, code desription and set remarks.
        /// 2/ Grouped by codename with amount '1' and the grand total of this code. All invoice lines of this code will be totaled and shown on 
        /// the invoice with qty1 and total cost of this code. No remarks are shown.
        /// 3/ Grouped by code name and price. This will group all invoice lines with the same codename and price and description. 
        ///     
        ///     E.g. If you book 4 codes CAF:
        ///     1: 1x CAF, 1.25 Eur, remarks: Coffee
        ///     2: 1x CAF, 1.25 Eur, remarks: Coffee
        ///     3: 1x CAF, 2.00 Eur, remarks: Coffee
        ///     4: 1x CAF, 1,25 Eur, remarks: Mineral Water
        ///     
        ///     Using option 1, these will be shown on the invoice as:
        ///     
        ///     Code:       Description                 Qty:    Price:      Total:
        ///     CAF         Cafetaria (Coffee)          1       1.25        1.25
        ///     CAF         Cafetaria (Coffee)          1       1.25        1.25
        ///     CAF         Cafetaria (Coffee)          1       2.00        2.00
        ///     CAF         Cafetaria (Mineral Water)   1       1.25        1.25
        ///
        ///     Using option 2, these will be shown on the invoice as:
        ///     
        ///     Code:       Description                 Qty:    Price:      Total:
        ///     CAF         Cafetaria                   1       5.75        5.75
        ///     
        ///     Using option 3, these will be shown on the invoice as:
        ///     
        ///     Code:       Description                 Qty:    Price:      Total:
        ///     CAF         Cafetaria                   2.00    1.25        2.50
        ///     CAF         Coffee                      1       2.00        2.00
        ///     CAF         Mineral Water               1       1.25        1.25
        ///
        /// </summary>
        public string Remarks;
        /// <summary>
        /// Set to Vendornumber for your application. this is created by the costumer. This code can be different for each installation.        
        /// </summary>
        public string VendorNumber;
        /// <summary>
        /// Optional: Set to a reference invoice number in your application. (Not currently used in CS software. For reference only)
        /// </summary>
        public string VendorInvoiceNumber;
        /// <summary>
        /// Optional: Set to a reference invoice date in your application. (Not currently used in CS software. For reference only)
        /// </summary>
        public DateTime? VendorInvoiceDate;
        /// <summary>
        /// Optional: Set to a reference used in your application. Can be any string. e.g. your internal ID of this cost/service/good booking. 
        /// (Not currently used in CS software. For reference only)
        /// </summary>
        public string VendorReference;
        /// <summary>
        /// Resident's National Number. (Only pass numeric characters.. please strip all '.', and '-' characters.)
        /// If resident ID is unknown when inserting, you can use the Resident's National Number to insert the code.
        /// If a Resident ID and a National Number is passed in the params, the Resident ID will be used to do the insert.
        /// National Number will only be used when ResidentID is passed as NULL.
        /// </summary>
        public string ResidentNatNumber;
    }
    /// <summary>
    /// Parameters for GetInvoiceCodes Request
    /// </summary>
    public class GetInvoiceCodesParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: Set to 'True' to only receive the list of actively used Invoice Codes. Set to 'False' to only receive the list of inactive
        /// invoice codes. Set to null to receive both.
        /// </summary>
        public bool? Active;
    }
    #endregion Invoicing

    #region Redirects
    /// <summary>
    /// Parameters for GetRedirectURLForResident method
    /// </summary>
    public class GetRedirectURLForResidentParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
         /// <summary>
        /// Identifier/Name of the redirect you want to use.
        /// </summary>
        public string RedirectName;
        /// <summary>
        /// ID of the resident you want to be redirected for.
        /// </summary>
        public int ResidentID;         
    }
    /// <summary>
    /// Parameters for GetResidentIDsForRedirect method
    /// </summary>
    public class GetResidentIDsForRedirectParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Identifier/Name of the redirect you want to use.
        /// </summary>
        public string RedirectName;
    }
    #endregion 

    #region WaitingList
    /// <summary>
    /// Parameters for EditWaitingListEntry method
    /// </summary>
    public class EditWaitingListEntryParams
    {   
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Unique ID of the waitinglist entry. Leave NULL to create a new entry. Specify an existing ID to update an existing Entry.
        /// </summary>
        public Int32? ID;
        /// <summary>
        /// Text that specifies why the resident needs to be / would like to be admitted.
        /// </summary>
        public string ReasonForAdmission;
        /// <summary>
        /// Specifies when the resident was enlisted on the institutions waiting list.
        /// </summary>
        public DateTime? EnlistDate;
        /// <summary>
        /// Resident's name
        /// </summary>
        public string Name;
        /// <summary>
        /// Residents firstname
        /// </summary>
        public string Firstname;
        /// <summary>
        /// Resident's domicile street and number
        /// </summary>
        public string Address;
        /// <summary>
        /// Resident's domicile zip code.
        /// </summary>
        public string ZipCode;
        /// <summary>
        /// Resident's domicile town
        /// </summary>
        public string Town;
        /// <summary>
        /// Resident's sex. (false: female, true: male, null: unknown.)
        /// </summary>
        public bool? Sex;
        /// <summary>
        /// Resident's date of birth
        /// </summary>
        public DateTime? DateOfBirth;
        /// <summary>
        /// Primary phone number
        /// </summary>
        public string Phone;
        /// <summary>
        /// Name of resident's partner
        /// </summary>
        public string PartnerName;
        /// <summary>
        /// Firstname of resident's partner.
        /// </summary>
        public string PartnerFirstName;
        /// <summary>
        /// Value describing the residents civil state. This is a LookupValue of Type 3.
        /// </summary>
        public byte? CivilState;
        /// <summary>
        /// Indicates if the admission is urgent. (null: unknown)
        /// </summary>
        public bool? AdmissionIsUrgent;
        /// <summary>
        /// Describes which type of roon is preffered. (Free text value... used for info only)
        /// </summary>
        public string RoomType;
        /// <summary>
        /// Specifies any remarks made during enlistment on the WaitingList.
        /// </summary>
        public string Remarks;
        /// <summary>
        /// Does this waitinglist entry specifies a shortstay period? (null: unknown)
        /// </summary>
        public bool? IsShortStay;        
        /// <summary>
        /// Status of this waitinglist entry. Get different possiblestatusses with GetWaitingListStatusList. Users define these. Different in each carehome.
        /// </summary>
        public short? WaitingListStatus;
        /// <summary>
        /// Free text field containt a brief medical anamnesis for the resident.
        /// </summary>
        public string MedicalAnamnesis;
        /// <summary>
        /// Defines if the admission is a preventive action.
        /// </summary>
        public bool? AdmissionIsPreventive;
        /// <summary>
        /// Resident's domicile country. Specified by country code. (See GetCountryCodes method)
        /// </summary>
        public short? Country;
        /// <summary>
        /// Specifies nationality of the resident. Specified by country code (See GetCountryCodes method)
        /// </summary>
        public short? Nationality;
        /// <summary>
        /// Resident's national number
        /// </summary>
        public string NatNumber;
        /// <summary>
        /// Resident's primary email address.
        /// </summary>
        public string Email;
        /// <summary>
        /// Prferred language for this resident. /// Language ID. Default: 1 is Dutch / 2 French
        /// </summary>
        public byte? PreferredLanguage;
        /// <summary>
        /// DateTime when effective admission is planned.
        /// </summary>        
        public DateTime? PlannedAdmissionDate;
        /// <summary>
        /// Unique ID of the planned room for this admission . (See GetRoomList method.)
        /// </summary>
        public short? PlannedRoom;
    }
    /// <summary>
    /// Parameters for GetWaitingListEntries method
    /// </summary>
    public class GetWaitingListEntriesParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: Set to comma separated list of the WaitingListEntryID's you want te retrieve. Set null to retrieve all entries.        
        /// </summary>
        public string WaitingListEntryID;
        /// <summary>
        /// Filter: Set to WaitingListStatusID to retrieve only entries with specified status. Leave null to retrieve all.
        /// </summary>
        public short? WaitingListStatus;
    }
    /// <summary>
    /// Parameters for EditWaitingListContact method
    /// </summary>
    public class EditWaitingListContactParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// ID of the WaitingListEntry this contact belongs to. (See WaitingListEntry object)
        /// </summary>
        public int WaitingListEntryID;
        /// <summary>
        /// Currently we support only 2 contacts per WaitingListEntry. This value specifies if this contact is #1 or #2
        /// </summary>        
        public short ContactNumber;
        /// <summary>
        /// Contact's Name
        /// </summary>        
        public string Name;
        /// <summary>
        /// Contact's first name
        /// </summary>    
        public string FirstName;
        /// <summary>
        /// Contact's Address
        /// </summary>                
        public string Address;
        /// <summary>
        /// Contact's date of birth
        /// </summary>       
        public DateTime? BirthDate;
        /// <summary>
        /// Contact's domicile country. Specified by country code. (See GetCountryCodes method)
        /// </summary>        
        public short? Country;
        /// <summary>
        /// Specifies Nationality of the contact. Specified by country code (See GetCountryCodes method)
        /// </summary>
        public short? Nationality;
        /// <summary>
        /// Contact's national number
        /// </summary>
        public string NatNumber;
        /// <summary>
        /// Contact's first phone number
        /// </summary>
        public string Phone1;
        /// <summary>
        /// Contact's second phone number
        /// </summary>
        public string Phone2;
        /// <summary>
        /// Prferred language for this contact. /// Language ID. Default: 1 is Dutch / 2 French
        /// </summary>
        public byte? PreferredLanguage;
        /// <summary>
        /// Contact's relation to the resident. Specified by a LookupValue. (LookupValue type 87)
        /// </summary>
        public byte? RelationToResident;
        /// <summary>
        /// Contact's address town
        /// </summary>
        public string Town;
        /// <summary>
        /// Contact's address Zip Code
        /// </summary>
        public string ZipCode;
        /// <summary>
        /// Preferred communicationmethod for this contact. 
        /// 0: By Letter
        /// 1: By Email
        /// 2: By Phone
        /// </summary>
        public short? PreferredCommunicationMethod;
    }
    /// <summary>
    /// Parameters for RemoveWaitingListContact method
    /// </summary>
    public class RemoveWaitingListContactParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// ID of the WaitingListEntry this contact belongs to. (See WaitingListEntry object)
        /// </summary>
        public int WaitingListEntryID;
        /// <summary>
        /// Currently we support only 2 contacts per WaitingListEntry. This value specifies if this contact is #1 or #2
        /// </summary>        
        public short ContactNumber;
    }

    /// <summary>
    /// Parameters for the GetWaitingListStatus call
    /// </summary>
    public class GetWaitingListStatusParams
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
    }


#endregion WaitingList

    #region FallReg
    /// <summary>
    /// Parameters for GetFallIncidents method
    /// </summary>
    public class GetFallIncidentsParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Filter: Pass a comma separated list of ResidentID's to retrieve only the fall incidents for specific residents. Leave null to retrieve all.
        /// </summary>
        public string ResidentIDList;
        /// <summary>
        /// Set to true to retrieve only active incidents, set to false to retrieve only inactive incidents, leave null to retrieve all incidents.
        /// </summary>
        public Boolean? ActiveOnly;
        /// <summary>
        /// Set to a datetime value to retrieve only incidents with a FallTimeStamp newer or equal then FromDate. Leave null to retrieve all.
        /// </summary>
        public DateTime? FromDate;
        /// <summary>
        /// Set to a datetime value to retrieve only incidents with a FallTimeStamp older or equal then UntilDate. Leave null to retrieve all.
        /// </summary>
        public DateTime? UntilDate;
    }
    /// <summary>
    /// Parameters for RegisterFallIncident method.
    /// </summary>
    public class RegisterFallIncidentParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Unique ID of the resident who fell.
        /// </summary>
        public int ResidentID;
        /// <summary>
        /// UserID of user that is registering the falling incident. Leave NULL to use current session's user.
        /// </summary>
        public int UserID;        
        /// <summary>
        /// LookUpValue ID of Type 31 (-> See GetLookUpValue method) defining the place where the resident fell.
        /// eg. Room, hallway, elevator... When set to ID 9 (other) you can use the text in LocationOther. (Fall was registered using a non standard/listed location)
        /// Value is Required!
        /// </summary>
        public int Location;
        /// <summary>
        /// When Location is set to 9 (other)  this property can be used to describe the location where the resident fell. Cannot be used when Location is not 9
        /// </summary>
        public string LocationOther;
        /// <summary>
        /// Lookupvalue ID of Type 32 (-> See GetLookUpValue method) defining the nature of the fall. Eg. resident fell out of bed, fel out of chair...
        /// When set to ID 6 (other) you can use the text in NatureOfFallOther. (Fall was registered using a non standard/listed nature)
        /// </summary>
        public int NatureOfFall;
        /// <summary>
        /// When NatureOfFall is set to 6 (other) this property can be used to describe the nature of the fall. Cannot be used when NatureOfFall is not 6
        /// </summary>
        public string NatureOfFallOther;
        /// <summary>
        /// List of LookupValue ID's of type 33 (-> See GetLookUpValue method) defining who was with the resident when he/she fell. 
        /// eg. doctor was present, nurse was present. Multiple persons can be specfied here. When you including 11 (other) you can use the text in
        /// PeoplePresentOther. (Fall was registered using a non standard/listed people present)
        /// When 0 (nobody present) is passed, no other persons can be registered as present. So the passed list of integerss can only contain 0.
        /// </summary>
        public List<int> PeoplePresentOnFall;
        /// <summary>
        /// When PeoplePresentOnFall icludes 11 (other) this property can be used to describe the nature of the fall.
        /// </summary>        
        public string PeoplePresentOnFallOther;
        /// <summary>
        /// If the resident was injured during the incident, this property can be set to a lookupvalue ID (type 34) of the sustained injury's type.
        /// eg. No Injury (0), No visible injury, Injury severity class 1 slight bruising, mild pain or wounds with little or no care (1),...
        /// </summary>
        public int InjuryByFall;
        /// <summary>
        /// LookupValue of ID type 35 (-> See GetLookupValue method) defining what was the probable cause of the fall (eg. clothing, bad shoes,....)
        /// When set to ID 7 (other) you can use the text in SuspectedCauseOther. (Fall was registered using a non standard/listed SuspectedCause)        
        /// </summary>
        public int SuspectedCause;
        /// <summary>
        /// When SuspectedCause is set to 7 (other) this property can be used to describe the suspected Cause of the fall. Cannot be used when SuspectedCause is not 7
        /// </summary>                
        public string SuspectedCauseOther;
        /// <summary>
        /// Defines if a resident was using freedom restrictive mesures on the moment of the incident. 
        /// (eg. fixated in a wheelchair, bed bars were used to preven falling out of bed,...)
        /// Set to false when resident was not fixated set to true of resident was fixated.
        /// </summary>
        public bool WasUsingFreedomRestrictedMeasures;
        /// <summary>
        /// Describes which freedom restricted measures were used.
        /// </summary>
        public string FreedomRestrictedMeasureDesc;
        /// <summary>
        /// Defines if family was informed about the falling incident. Set to true if family was present during fall.
        /// </summary>
        public bool FamilyWasInformed;        
        /// <summary>
        /// Defines if the resident's doctor was informed about the falling incident. Set to true if doctor was informed.
        /// </summary>
        public bool DoctorWasInformed;
        /// <summary>
        /// LookupValue of ID type 47 (-> See GetLookupValue method) If a resident has any known medical history that might lead to this falling incident, the nature of this history is defined 
        /// using this property. (eg. In case of Reduced vision, Hearing difficulty, Walking difficulty...)
        /// </summary>
        public int HistoricalBackground;
        /// <summary>
        /// Timestamp of the actual falling incident.
        /// </summary>
        public DateTime FallTimeStamp;
        /// <summary>
        /// Any addtional remarks can be specified here.
        /// </summary>
        public string Remarks;
        /// <summary>
        /// You can link the fall incident to all the different modules in our software. If you link anthis incident to a CS Software module, an observation will be made
        /// and will be visible to the users of that module. (See GetModlinkables method)
        /// </summary>
        public string LinkToModules;
        /// <summary>
        /// When registering a fall incident, automatically an observation will be made to the Care Home's diary. If explicitly do NOT want to have an observation created in the diary.
        /// Set this property to False. All other values (null, true) will result in the creation of an observation in the Care Home's diary.       
        /// </summary>
        public bool? InDiary;
    }
    #endregion

    #region AppNotifications
    /// <summary>
    /// Parameters for GetCurrentAppNotification method
    /// </summary>
    public class GetCurrentAppNotificationsParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;        
    }
    /// <summary>
    /// Parameters for EditAppNotificationMethod
    /// </summary>
    public class EditAppNotificationParameters
    {   
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Guid of the AppNotification you want to edit.
        /// If you want to create a new AppNotification, leave this property NULL.
        /// </summary>
        public Guid? ID;
        /// <summary>
        /// Notification message. This is the message that will be shown to the users.
        /// </summary>
        public string Text;
        /// <summary>
        /// For icons we use the public http://glyphicons.com/ library. This property let's you choose what icon you want us to show with your AppNotification.
        /// Classname of icon you want to show with the message. If left blank, glyphicons-info-sign 
        /// </summary>
        public string IconClass;
        /// <summary>
        /// Specifies what colorcode will be used when displaying the AppNotification in our Apps.
        /// </summary>
        public string Color;
        /// <summary>
        /// Optional: if you want to link a specific room to this Appnotification, set the corresponding RoomID here.
        /// </summary>
        public int? LinkedRoomID;
        /// <summary>
        /// Optional: if you want to link a specific resident to this AppNotification, set the corresponding LinkedResidentID here.
        /// </summary>
        public int? LinkedResidentID;
        /// <summary>
        /// If you want the AppNotification to only appear for a certain departments. You can set the corresponding departmet IDs here by passing a comma separated ID list as a string.
        /// (E.g. "1,4,13")
        /// </summary>
        public string ShowOnlyForDepartmentIDs; //meerdere mogelijk
        /// <summary>
        /// Amount of seconds before this notification expires since creation. If the AppNotification is considered expired, it will no longer be kept in the servercache
        /// and the AppNotification will no longer be shown. Max TimeToLive = 600s (10min) If a higher number of seconds is passed, the notification will automatically be saved
        /// with a TimeToLive of 600s
        /// </summary>
        public int TimeToLiveInSec;      
    }
    /// <summary>
    /// Parameters for RemoveAppNotification method
    /// </summary>
    public class RemoveAppNotificationParameters
    {
        /// <summary>
        /// User's SessionID received after successful Login.
        /// </summary>
        public string SessionID;
        /// <summary>
        /// Guid of the AppNotification you want to remove.
        /// </summary>
        public Guid ID;       
    }
    #endregion 


}



