using System.Collections.Generic;
using System;
namespace WZS.Common
{

    #region Utilities & Helpers

    /// <summary>
    /// Result of DateTimeToCaremoment function
    /// </summary>
    public class DateTimeAsCareMoment
    {
        /// <summary>
        /// Resulting Care Date.
        /// </summary>
        public System.DateTime CareDate { get; set; }
        /// <summary>
        /// Resulting Caremoment.
        /// </summary>
        public int CareMomentID { get; set; }
    }

    /// <summary>
    /// Defines the status of a DBImage. (Senior picture, wound picture... etc.. are all stored in the database as Base64 encoded strings and can be accessed
    /// through the WZSN.net web service. Images can be directly downloaded from a hosted image cache or can bet retrieved using the provided methods.
    /// Also contains the URL on which this image is available.
    /// </summary>
    public class ImageStatus
    {
        /// <summary>
        /// Unique identifier of the image
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ID of the object the image is linked to. (A certain senior, a certain wound,...)
        /// </summary>
        public int LinkID { get; set; }
        /// <summary>
        /// Date on which the image was last modified.
        /// </summary>
        public System.DateTime ModifiedOn { get; set; }
        /// <summary>
        /// URL on which the image is available for download.
        /// </summary>
        public string URL { get; set; }
    }

    /// <summary>
    /// DB Image class represents an image, stored in the database.
    /// (Senior picture, wound picture... etc.. are all stored in the database as a Base64 encoded JPG. DB Images and 
    /// can be accessed through the WZSN.net web service. Images can be directly downloaded from a hosted image cache (as actual .JPG) 
    /// or can be retrieved using the provided methods.
    /// </summary>
    public class DBImage
    {
        /// <summary>
        /// Unique identifier of the image
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ID of the image is linked to. (Senior, Wounds,...)
        /// </summary>
        public int LinkID { get; set; }
        /// <summary>
        /// BAse64 encoded string representing the JPEG contents.
        /// </summary>
        public string ImageData { get; set; }
    }

    /// <summary>
    /// Object containing a fixed text (as used in our applications) in a specific language. Can be used for translations.
    /// </summary>      
    public class FixedText
    {
        /// <summary>
        /// Unique Identifier for text.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Language ID. Default: 1 is Dutch / 2 French
        /// </summary>
        public int LanguageID { get; set; }
        /// <summary>
        /// Actual text.
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// Object containing a single ID Value. E.g. Used when methods return a value for a successfully inserted record.
    /// </summary>
    public class IDValue
    {
        /// <summary>
        /// Constructor for IDValue, without set value
        /// </summary>
        public IDValue()
        {
        }
        /// <summary>
        /// Constructor for IDValue, with set value.
        /// </summary>
        /// <param name="i"></param>
        public IDValue(int i)
        {
            ID = i;
        }
        /// <summary>
        /// Returned ID value as integer.
        /// </summary>
        public System.Int32? ID { get; set; }
    }
    /// <summary>
    ///  Object containing a count Value. E.g. used when methods return the number of effected db records.
    /// </summary>
    public class CountValue
    {
        /// <summary>
        /// Actual count value as integer.
        /// </summary>
        public System.Int32? Count { get; set; }
    }
    /// <summary>
    /// Contains the datetime on which an object (senior, image,....) was last modified.
    /// </summary>
    public class ModifiedObject
    {
        /// <summary>
        /// Unique ID of the object
        /// </summary>
        public Int32 ObjectID { get; set; }
        /// <summary>
        /// Last modification Date Time
        /// </summary>
        public DateTime ModifiedOn { get; set; }
    }

    /// <summary>
    /// Object defines a country code as used in the application
    /// </summary>
    public class CountryCode
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ISOCode as string. (BE, NL,...)
        /// </summary>
        public string ISOCode { get; set; }
        /// <summary>
        /// Country Name (België, Nederland,...)
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Object defines a lookupvalue as used in the application. The database stores drop down value-text pairs in a single table.
    /// All possible values are arranged by type. For instance if you want to retrieve all possible value/text pairs for possible marital states,
    /// request all lookupvalues for type 3. Look through the WZS.net API documentation to see which types are used where. If a lookuptype is used in
    /// a return type or method parameter, the type will be specified in the documentation snippet.
    /// </summary>
    public class LookupValue
    {
        /// <summary>
        /// Type ID of the LookupValue. (Lookupvalues are grouped by type)
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// Value used when registering this LookupValue
        /// </summary>
        public int ValueID { get; set; }
        /// <summary>
        /// Matchin display text as shown in our application. (Returned in chosen language)
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// If assigned, this property can be used to sort the LookupValues within this type group.
        /// </summary>
        public int? SortOrder { get; set; }
    }

    #endregion Utilities & Helpers

    #region Webservice technical
    /// <summary>
    /// Describes the user session details after a successful login.
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// User's first name
        /// </summary>
        public string firstName { get; set; }
        /// <summary>
        /// User's last name
        /// </summary>
        public string lastName { get; set; }
        /// <summary>
        /// Language of the user's session. (The default is taken form the configured user account's language
        /// in the WZD application's user management. All language specific string values returned by the web service 
        /// will automatically be translated in the language set in the session.
        /// </summary>
        public byte languageID { get; set; }
        /// <summary>
        /// Unique ID for the user account
        /// </summary>
        public short userID { get; set; }
        /// <summary>
        /// Unique GUID of the created session. Use this ID in all method parameters requiring a SessionID parameter.
        /// </summary>
        public string sessionID { get; set; }
        /// <summary>
        /// Application name that initiated the session. (Your application)
        /// </summary>
        public string SConName { get; set; }
        /// <summary>
        /// True: you are currently logged on to a test or acceptance environment.
        /// False: you are currently logged on to a client's production environment.
        /// </summary>
        public bool? LoggedOnToTestEnv { get; set; }
        /// <summary>
        /// This property contains a list of public server settings.
        /// These settings show how the client's web service is configured.
        /// See the PublicServerSettings Class.
        /// </summary>
        public PublicServersSettings ServerSettings { get; set; }
        /// <summary>
        /// If your application can have different installed features that optionally be installed they will be listed here.
        /// </summary>
        public List<string> Availablefeatures { get; set; }
        /// <summary>
        /// Active optional Care Solutions licenses installed for this client/care home. Not applicable for thirds party users.
        /// </summary>
        public List<string> ActiveWZXLicenses { get; set; }
    }

    /// <summary>
    /// These settings show how the client's web service is configured.
    /// </summary>
    public class PublicServersSettings
    {
        /// <summary>
        /// Session Timeout in minutes how long a session will remain active on the server.
        /// Timeout counter resets every time a request is made with the corresponding session ID.
        /// </summary>
        public int ServerSessionTimeOut { get; set; }

        /// <summary>
        /// Info setting. This setting indicates after how many minutes client applications should lock/timeout/require a new login.
        /// This setting is set by the customer. WZS.Net does not use this value.
        /// </summary>
        public int ClientSessionTimeOut { get; set; }

        /// <summary>
        /// If the notification system is running on this webserver, this settings defines the interval in between a notifications check/run 
        /// is made.
        /// Notifications can be: web service system heartbeat, Web service Startup notification, ….
        /// If this setting is set to 0, the notification thread is disabled.
        /// </summary>
        public int CheckMinutes { get; set; }

        /// <summary>
        /// Server's installation Unique ID. Can be used for reference. 
        /// Will also be mentioned in the notifications created by the notification thread above
        /// </summary>
        public string ServerInstallationID { get; set; }

        /// <summary>
        /// TCP port on which the web service is listening
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// True: web service must be accessed using SSL -> https://
        /// False: web service is not using SSL -> http://
        /// </summary>
        public bool UseSSL { get; set; }

        /// <summary>
        /// url on which the server is available. (Does not include port)
        /// </summary>
        public string ServerHostUrl { get; set; }

        /// <summary>
        /// Version number of the currently installed CS WZS.net software.
        /// </summary>
        public Version ServerVersion { get; set; }
    }

    /// <summary>
    /// Describes a WZD Application Database accessible through the web service
    /// Each care home/institution has a separate application running on its own database.
    /// The WZS.Net web service is a single install that can access multiple WZD Databases.
    /// </summary>
    public class WZDatabase
    {
        /// <summary>
        /// Unique ID of the WZD Database.
        /// </summary>
        public int WZD_ID { get; set; }

        /// <summary>
        /// Database's Internal name.
        /// </summary>
        public string WZD_Name { get; set; }

        /// <summary>
        /// Database's display name. User friendly name for the database.
        /// Used as username suffix in the login method to determine which care home the session will set up for, and thus which database will be accessed.
        /// This name can also be used if you need a dropdown where a user can choose a care home to log in to. (See connections method.)
        /// </summary>
        public string WZD_DisplayName { get; set; }

        /// <summary>
        /// full database's SQL Connection string.
        /// </summary>
        public string WZD_ConnectionString { get; set; }

        /// <summary>
        /// Timestamp when the last notifications run was executed on this database. (If the notification thread is enabled.)
        /// </summary>
        public System.DateTime WZD_LastNotificationCheck { get; set; }

    }

    /// <summary>
    /// Describes database details for internal login procedures. not publicly used.
    /// </summary>
    public class SconDB
    {
        /// <summary>
        /// WZDatabase ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Database Name
        /// </summary>
        public string dbName { get; set; }
        /// <summary>
        /// Database Displayname
        /// </summary>
        public string dbDisplayName { get; set; }
        /// <summary>
        /// Timestamp when the last notifications run was executed on this database. (If the notification thread is enabled.)
        /// </summary>
        public System.DateTime? lastCheckedfornotifs { get; set; }
        /// <summary>
        /// WZS.Net version this database is configured for.
        /// </summary>
        public string version { get; set; }
    }

    #endregion Webservice technical

    #region  Rooms and Departments

    /// <summary>
    /// Describes a department of a care home.
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Department name
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Department's phone number
        /// </summary>
        public string Phone { get; set; }
    }

    /// <summary>
    /// Describes a room of a care home
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Unique ID. (Not the room name)
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Room name
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Department the room belongs to
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// Field used for external reference. External ID as used in e.g. third party room terminals.
        /// Can contain any ID/code used by third party software to reference the room.
        /// </summary>
        public string NeuronID { get; set; }
        /// <summary>
        /// Specifies the Roomstyle of this room.
        /// </summary>
        public int? RoomtypeID { get; set; }
        /// <summary>
        /// Specifies the Roomstyle of this room.
        /// </summary>
        public int? RoomstyleID { get;set;}
        /// <summary>
        /// Specifies if this room is a room dedicated for shortstay. (when true, room is a shortstay room)
        /// </summary>
        public bool ShortStayRoom {get;set;}
    }

    /// <summary>
    /// Describes a roomtype avaliable in the application. Users need to define a RoomType for a room when creating 
    /// This is a hardcoded fixed list. Equal among all customers. (E.g. Flat, Room)
    /// </summary>
    public class Roomtype
    {        
        /// <summary>
        /// Unique ID of the roomtype
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name of the roomtype. Returned in the session's language.
        /// </summary>
        public string Name { get; set;}
    }

    /// <summary>
    /// Describes a roomstyle avaliable in the application. Users can define an optional Roomstyle for a room. 
    /// This is a user definable list. Different among all customers.  (e.g. Corner room, Luxury room,...)
    /// </summary>
    public class Roomstyle
    {
        /// <summary>
        /// Unique ID of the roomstyle
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name of the roomstyle. Returned in the session's language.
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Occupancy status of a room for a single day.
    /// </summary>
    public class RoomOccupancyStatus
    {
        /// <summary>
        /// Date of occupancy
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// ID of the department the room belongs to
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// Name of the department the room belongs to
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// Room ID
        /// </summary>
        public int RoomID { get; set; }
        /// <summary>
        /// Room name
        /// </summary>
        public string RoomDesc { get; set; }
        /// <summary>
        /// ID of roomtype. (Condo, single room,....)
        /// </summary>
        public int? RoomTypeID { get; set; }
        /// <summary>
        /// Name of roomtype
        /// </summary>
        public string RoomTypeDesc { get; set; }
        /// <summary>
        /// Name of the roomstyle. (User defined in WZD. E.g. 'luxe kamer' etc... User can customize roomstyles.)
        /// </summary>
        public int? RoomStyleID { get; set; }
        /// <summary>
        /// ID of the roomstyle
        /// </summary>
        public string RoomStyleDesc { get; set; }
        /// <summary>
        /// Room's capacity
        /// </summary>
        public byte? Capacity { get; set; }
        /// <summary>
        /// Room's occupancy on given date.
        /// </summary>
        public int Occupancy { get; set; }
    }

    #endregion  Rooms and Departments

    #region Residents & Resident info

    /// <summary>
    /// Describes a contact person for a resident. Contacts can be linked to 1 resident only. 
    /// If one physical contact person needs to be linked to different residents, a new record will be created for each contact.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Unique ID of the contact
        /// </summary>
        public int ContactID { get; set; }
        /// <summary>
        /// Unique ID of the resident the contact belongs to.
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// Contact's last name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Contact's first name.
        /// </summary>
        public string Firstname { get; set; }
        /// <summary>
        /// Contact's address. (Street + Number)
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Contact's Zip code
        /// </summary>
        public string Zipcode { get; set; }
        /// <summary>
        /// Contact's Town
        /// </summary>
        public string Town { get; set; }
        /// <summary>
        /// Contact's country name
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Primary phone number
        /// </summary>
        public string Phone1 { get; set; }
        /// <summary>
        /// Secondary phone number
        /// </summary>
        public string Phone2 { get; set; }
        /// <summary>
        /// Relation the contact has with the resident.
        /// </summary>
        public string Relation { get; set; }
        /// <summary>
        /// Contact's email address.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Contact's ID card number.
        /// </summary>
        public string IDNumber { get; set; }
        /// <summary>
        /// Should contact be notified when a resident has deceased?
        /// </summary>
        public bool NotifyOnDecease { get; set; }
        /// <summary>
        /// Should contact be notified when a resident experiences an emotional crisis?
        /// </summary>
        public bool NotifyOnEmotionalCrisisResident { get; set; }
        /// <summary>
        /// Should contact be notified when a family issues occur?
        /// </summary>
        public bool NotifyOneEmotionalCrisisFamily { get; set; }
        /// <summary>
        /// Order of contact. Lower numbers will be contacted first by the nurse home.
        /// </summary>
        public int ContactOrder { get; set; }
    }


    /// <summary>
    /// Describes a treatment directive for a resident. 
    /// A treatment directive describes what precautions/actions a resident has requested, agreed or not agreed to in case of hospitalization, severe sickness or injury...
    /// E.g. describes if a resident still wants to have CPO administered, still wants to be treated in the hospital, ...
    /// </summary>
    public class ResidentTreatmentDirective
    {
        /// <summary>
        /// ID of the directive
        /// </summary>        
        public int DirectiveID { get; set; }
        /// <summary>
        /// Description of the directive
        /// </summary>
        public string DirectiveDesc { get; set; }
        /// <summary>
        /// Resident specific remarks for the directive.
        /// </summary>
        public string DirectiveRemarks { get; set; }
        /// <summary>
        /// Date after which the directive was agreed to be used on.
        /// </summary>
        public System.DateTime DateFrom { get; set; }
    }

    /// <summary>
    /// Describes a disease a resident is suffering from. Multiple diseases per resident are possible.
    /// </summary>
    public class ResidentDisease
    {
        /// <summary>
        /// Unique ID of the disease/resident record
        /// </summary>
        public int ResidentDiseaseID { get; set; }
        /// <summary>
        /// Unique ID of the resident suffering the disease
        /// </summary>            
        public int ResidentID { get; set; }
        /// <summary>
        /// Begin date of the disease
        /// </summary>
        public System.DateTime Begindate { get; set; }
        /// <summary>
        /// End date of the disease. Resident is no longer suffering this disease.
        /// </summary>
        public System.DateTime Enddate { get; set; }
        /// <summary>
        /// Unique ID of the Disease
        /// </summary>
        public int DiseaseID { get; set; }
        /// <summary>
        /// Name of the disease
        /// </summary>
        public string DiseaseDesc { get; set; }
        /// <summary>
        /// Treatment protocol for the disease
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// Place where the disease was contracted. (E.g. Nurse home, vacation,...)
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Resident is kept in quarantine.
        /// </summary>
        public bool Quarantaine { get; set; }
        /// <summary>
        /// Resident is colonized with the disease
        /// </summary>
        public bool Colonisation { get; set; }
    }


    /// <summary>    
    /// Describes a medical remark object for a resident. 
    /// (Obsolete, use ResidentMedicalRemark_v2 instead.)
    /// </summary>
    [Obsolete("Class is deprecated, use ResidentMedicalRemark_v2 instead", false)]
    public class ResidentMedicalRemark
    {
        /// <summary>
        /// Unique ID of the resident the remark belongs to
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// Describes if the resident is an active resident in the care home. Inactive residents are resident records that have been archived.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Resident’s last name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Resident's first name.
        /// </summary>
        public string Firstname { get; set; }
        /// <summary>
        /// Unique ID of Resident's room
        /// </summary>
        public int RoomID { get; set; }
        /// <summary>
        /// Name of resident's room
        /// </summary>
        public string RoomDescription { get; set; }
        /// <summary>
        /// Unique ID of resident's department
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// Name of resident's department
        /// </summary>
        public string DepartmentDescription { get; set; }
        /// <summary>
        /// Resident's blood type
        /// </summary>
        public string Bloodtype { get; set; }
        /// <summary>
        /// DO NOT USE! This version of the class has an incorrect resident's rhesus factor. Please use _V2
        /// Resident rhesus true: positive, false: negative or unknown. (should be nullable here...)
        /// </summary>
        public bool Rhesus { get; set; }
        /// <summary>
        /// Resident's latest WZD KATZ Score category for resident. 
        /// </summary>        
        public string WZDKatzScore { get; set; }
        /// <summary>
        /// Unique ID of doctor treating resident.
        /// </summary>
        public int DoctorID { get; set; }
        /// <summary>
        /// Name, first name and phone number of doctor
        /// </summary>
        public string DoctorInfo { get; set; }
        /// <summary>
        /// Resident is diabetic
        /// </summary>
        public bool IsDiabetic { get; set; }
        /// <summary>
        /// Resident is epileptic
        /// </summary>
        public bool IsEpileptic { get; set; }
        /// <summary>
        /// Resident suffers from a heart condition.
        /// </summary>
        public bool HasAngor { get; set; }
        /// <summary>
        /// Resident has a pacemaker
        /// </summary>
        public bool HasPacemaker { get; set; }
        /// <summary>
        /// Resident suffers from a coagulation disorder
        /// </summary>
        public bool HasCoagulationDisorder { get; set; }
        /// <summary>
        /// Resident suffer from Chronic Obstructive Pulmonary Disease. (Lung conditions)
        /// </summary>
        public bool HasCOPD { get; set; }
        /// <summary>
        /// Resident suffers from dementia
        /// </summary>
        public bool HasDemention { get; set; }
        /// <summary>
        /// States level of dementia if applicable.
        /// </summary>
        public string Dementionlevel { get; set; }
        /// <summary>
        /// String describing the non-food allergies of a resident.
        /// </summary>
        public string Allergies { get; set; }
        /// <summary>
        /// String describing any resident's food allergies.
        /// </summary>
        public string FoodAllergies { get; set; }
        /// <summary>
        /// Resident is palliative
        /// </summary>
        public bool IsPaliative { get; set; }
        /// <summary>
        /// Resident does not want to be resuscitated. When false, resident wants to be resuscitated, when true resident does NOT want to be resuscitated.
        /// </summary>
        public bool DNR { get; set; }

    }

    /// <summary>    
    /// Describes a medical remark object for a resident. 
    /// </summary>
    public class ResidentMedicalRemark_v2
    {
        /// <summary>
        /// Unique ID of the resident the remark belongs to
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// Describes if the resident is an active resident in the care home. Inactive residents are resident records that have been archived.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Resident’s last name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Resident's first name.
        /// </summary>
        public string Firstname { get; set; }
        /// <summary>
        /// Unique ID of Resident's room
        /// </summary>
        public int RoomID { get; set; }
        /// <summary>
        /// Name of resident's room
        /// </summary>
        public string RoomDescription { get; set; }
        /// <summary>
        /// Unique ID of resident's department
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// Name of resident's department
        /// </summary>
        public string DepartmentDescription { get; set; }
        /// <summary>
        /// Resident's bloodtype
        /// </summary>
        public string Bloodtype { get; set; }
        /// <summary>
        /// Resident rhesus true: positive, false: negative, null: Unknown
        /// </summary>
        public bool? Rhesus { get; set; }
        /// <summary>
        /// Resident's latest WZD KATZ Score category for resident. 
        /// </summary>    
        public string WZDKatzScore { get; set; }
        /// <summary>
        /// Unique ID of doctor treating resident.
        /// </summary>
        public int DoctorID { get; set; }
        /// <summary>
        /// Name, first name and phone number of doctor
        /// </summary>
        public string DoctorInfo { get; set; }
        /// <summary>
        /// Resident is diabetic
        /// </summary>
        public bool IsDiabetic { get; set; }
        /// <summary>
        /// Resident is epileptic
        /// </summary>
        public bool IsEpileptic { get; set; }
        /// <summary>
        /// Resident suffers from a heart condition.
        /// </summary>
        public bool HasAngor { get; set; }
        /// <summary>
        /// Resident has a pacemaker
        /// </summary>
        public bool HasPacemaker { get; set; }
        /// <summary>
        /// Resident suffers has from coagulation disorder.
        /// </summary>
        public bool HasCoagulationDisorder { get; set; }
        /// <summary>
        /// Resident suffer from Chronic Obstructive Pulmonary Disease. (Lung conditions)
        /// </summary>
        public bool HasCOPD { get; set; }
        /// <summary>
        /// Resident suffers from dementia
        /// </summary>         
        public bool HasDemention { get; set; }
        /// <summary>
        /// States level of above dementia if applicable.
        /// </summary>        
        public string Dementionlevel { get; set; }
        /// <summary>
        /// String describing the non-food allergies of a resident.
        /// </summary>
        public string Allergies { get; set; }
        /// <summary>
        /// String describing any resident's food allergies.
        /// </summary>
        public string FoodAllergies { get; set; }
        /// <summary>
        /// describes wether the resident is palliative.
        /// </summary>
        public bool IsPaliative { get; set; }
        /// <summary>
        /// Resident does not want to be resuscitated. When false, resident wants to be resuscitated, when true resident does NOT want to be resuscitated.
        /// </summary>
        public bool DNR { get; set; }
    }

    /// <summary>
    /// Describes a resident as known in CS Software and care home.
    /// </summary>
    public class Resident
    {
        /// <summary>
        /// unique identifier of the resident.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Resident's last name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Resident's first name.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Residents nickname or short name he/she prefers
        /// </summary>
        public string CallName { get; set; }
        /// <summary>
        /// Resident's date of birth
        /// </summary>
        public System.DateTime BD { get; set; }
        /// <summary>
        /// Resident's place of birth
        /// </summary>
        public string BP { get; set; }
        /// <summary>
        /// Unique ID  of the room the resident currently resides in.
        /// </summary>
        public int RoomID { get; set; }
        /// <summary>
        /// Resident's national number
        /// </summary>
        public string NatNumber { get; set; }
        /// <summary>
        /// Resident's sex. false: female, true: male.
        /// </summary>
        public bool Sex { get; set; }
        /// <summary>
        /// Unique ID of resident's nationalities country code. (See countrycode class.)
        /// </summary>
        public int Nationality { get; set; }
        /// <summary>
        /// Unique identifier of resident's treating doctor.
        /// </summary>
        public int DoctorID { get; set; }
        /// <summary>
        /// Timestamp when resident was created in CS Software.
        /// </summary>
        public System.DateTime Stamp { get; set; }
        /// <summary>
        /// Resident's mutuality number
        /// </summary>
        public string MutNo { get; set; }
        /// <summary>
        /// Resident's domicile street and number
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Resident's domicile zip code.
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// Resident's domicile town
        /// </summary>
        public string Town { get; set; }
        /// <summary>
        /// Name of the room the resident currently resides in.
        /// </summary>
        public string Room { get; set; }
        /// <summary>
        /// Name of resident's department.
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// Unique ID of resident's department.
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// Status of Resident. 
        /// -True: resident is marked as active in CS WZD software -
        /// -False: resident is marked as inactive in CS WZD
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Current presence state of resident:
        /// 1	Currently present
        /// 2	Absent (hospital)
        /// 3	Absent (family)        
        /// 4	Absent (resident is on holiday)
        /// 5	Absent (Deceased)
        /// 6	Absent (Left the care home/institution)
        ///7	Present (without mutuality rights)
        /// </summary>
        public int CurrentPresenceState { get; set; }
        /// <summary>
        /// Description of the presence state
        /// </summary>
        public string CurrentPresenceDesc { get; set; }
        /// <summary>
        /// Timestamp on which the resident was last modified
        /// </summary>
        public System.DateTime? LastModifiedOn { get; set; }
        /// <summary>
        /// Resident has agreed to share his information for an 'informed consent' for VitaLink
        /// </summary>
        public System.DateTime? VitaLinkInformedConsentSince { get; set; }
        /// <summary>
        /// Resident has agreed to share his information for an 'informed consent' for BelRai
        /// </summary>
        public System.DateTime? BelraiInformedConsentSince { get; set; }
    }

    /// <summary>
    /// Contains a measured parameter value for a specific resident. 
    /// Measured at a certain point in time.
    /// </summary>
    public class ResidentParameter
    {
        /// <summary>
        /// Unique ID for the resident parameter
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Linked database action ID.
        /// All parameters have a 'signed' action record in the database. This ID contains the ID of that record. 
        /// This is returned as a reference.
        /// </summary>
        public int ActionID { get; set; }
        /// <summary>
        /// Unique ID of the measured parameter. (See Parameter class)
        /// This defines which parameter has been measured. (Heart rate, temperature...)
        /// </summary>
        public int ParameterID { get; set; }
        /// <summary>
        /// Description/name of the measured parameter. ('Bloeddruk', 'Pols',...)
        /// </summary>
        public string ParameterDesc { get; set; }
        /// <summary>
        /// Measured value.
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// Unique ID of secondary paramdetail value. (If applicable) (See ParamDetails class)
        /// For some parameters you can choose to register a secondary value. Chosen from pre-defined, fixed list of parameterdetailvalues.
        /// E.g. for a pulse, you can register the measured pulse at 75 ppm. But you can also register ‘fast’ or ‘irregular’ as a second value
        /// Not all parameters have these secondary values.
        /// </summary>
        public int Value2 { get; set; }
        /// <summary>
        /// Description of the secondary paramvalue. (ParamDetail)
        /// </summary>
        public string ParamDetailDesc { get; set; }
        /// <summary>
        /// Remarks entered when registering the measured parameter.
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// Caredate when the value was measured.
        /// </summary>
        public System.DateTime ParamValueDate { get; set; }
        /// <summary>
        /// Unique ID of the resident this parameter was measured on
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// Care Moment ID in which this parameter was measured
        /// </summary>
        public int CareMomentID { get; set; }
        /// <summary>
        /// TimeStamp when this value was measured.
        /// </summary>
        public System.DateTime Stamp { get; set; }
    }

    /// <summary>
    /// Describes a vaccine that was administered to the resident.
    /// </summary>
    public class ResidentVaccination
    {
        /// <summary>
        /// Unique ID of this resident vaccination record
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Unique ID of the resident this vaccine was administered to.
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// Unique ID of the vaccination type that was administered. (See VaccinationTypes)
        /// Defines is it is e.g. a flue vaccine or tetanus or...
        /// </summary>
        public int VaccinationTypeID { get; set; }
        /// <summary>
        /// Any remarks registered when vaccine was administered.
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// Date of vaccination
        /// </summary>
        public DateTime? VaccAdminDate { get; set; }
        /// <summary>
        /// Date on which the administered vaccine will expire. (If applicable)
        /// </summary>
        public DateTime? VaccExpiryDate { get; set; }
        /// <summary>
        /// Is the last vaccination for this type for this resident?
        /// E.g. If the resident receives a yearly flue shot, this record indicates if this vaccination is the last flue vaccine he/she has received.
        /// </summary>
        public bool IsLastVacc { get; set; }

    }

    /// <summary>
    /// Describes a Document Type. When uploading/inserting resident documents (
    /// </summary>
    public class ResidentDocumentType
    {
        /// <summary>
        /// Unique ID of the DocumentType
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name of the DocumentType
        /// </summary>
        public string Name { get; set; }
    }

    #endregion Residents & Resident info

    #region Doctors & Treatment
    /// <summary>
    /// Describes a doctor's visit for a certain resident.
    /// </summary>
    public class DoctorVisit
    {
        /// <summary>
        /// Unique ID for this doctorvisit.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Timestamp of the doctorvisit.
        /// </summary>
        public System.DateTime VisitDate { get; set; }
        /// <summary>
        /// Unique ID of the resident the doctor visited.
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// the nomenclature code for this visit. Also determines price and percentage the resident has to pay.
        /// </summary>
        public int Nomenclature { get; set; }
        /// <summary>
        /// Total price invoiced for this visit
        /// </summary>
        public double PriceTotal { get; set; }
        /// <summary>
        /// Amount invoiced to the health service/mutuality
        /// </summary>
        public double PriceHealthService { get; set; }
        /// <summary>
        /// Amount invoiced to resident
        /// </summary>
        public double PricePatient { get; set; }
        /// <summary>
        /// Remarks registered for this visit.
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// Has the visit already been invoiced?
        /// </summary>
        public bool invoiced { get; set; }
    }


    /// <summary>
    /// A Nomenclature object describes an action that can be performed by a doctor while visiting a patient.
    /// </summary>
    [Obsolete("Class is deprecated. No longer used, refer to NomenclatureWithRateList", false)]
    public class Nomenclature
    {
        /// <summary>
        /// Unique ID of this nomenclature object
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Rate/price doctor invoices when performing this action
        /// </summary>
        public double Rate { get; set; }
        /// <summary>
        /// Date from which this rate can be used.
        /// </summary>
        public System.DateTime RateValidFrom { get; set; }
        /// <summary>
        /// Date until this rate can be used.
        /// </summary>
        public System.DateTime RateValidUntil { get; set; }
        /// <summary>
        /// Description/name of the visit action. e.g. 'thuisbezoek'
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// A Nomenclature object describes an action that can be performed by a doctor while visiting a patient.
    /// </summary>
    public class NomenclatureWithRateList
    {
        /// <summary>
        /// Unique ID of this nomenclature object
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Contains a list of NomenClatureRates valid over time for this particular Nomenclature/visit. (See NomenclatureRate)
        /// </summary>
        public List<NomenclatureRate> Rates { get; set; }
        /// <summary>
        /// Description/name of the visit action. e.g. 'thuisbezoek'
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Describes a rate that can be charged for a certain nomenclature. (See NomencaltureWithRateList)
    /// A single nomenclature item can have different rates over time. Prices are adjusted and can increase/decrease in time.
    /// </summary>
    public class NomenclatureRate
    {
        /// <summary>
        /// Date from which this rate can be applied
        /// </summary>
        public System.DateTime ValidFrom { get; set; }
        /// <summary>
        /// Date from until which this rate can be applied
        /// </summary>
        public System.DateTime ValidUntil { get; set; }
        /// <summary>
        /// Amount charged
        /// </summary>
        public double Rate { get; set; }
    }

    #endregion Doctors & Treatment

    #region Registrations, parameters and Observations

    /// <summary>
    /// Describes a parameter type available to register for a resident.
    /// A Parameter describes a measurement that can be registered for a resident. E.g. temperature, weight, length, pulse
    /// A Parameter always expect some kind of measured value when registering. (E.g. 36°C, 120cm,...) 
    /// For some parametertypes you can choose to register a secondary value. Chosen from pre-defined, fixed list of parameterdetailvalues.
    /// E.g. for a pulse, you can register the measured pulse at 75 ppm. But you can also register 'fast' or 'irregular' as a second value
    /// Not all parameters have these secondary values. -> See the ParamDetails object.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Unique ID of the parameter type
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name/Description of the parameter type.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// True: paramdetails are available for this parametertype. 
        /// False: the parameter only requires a single value to be registered. ParamDetails are not available for this parametertype.
        /// </summary>
        public bool HasDetails { get; set; }
        /// <summary>
        /// Unit of measurement for the parametertype
        /// </summary>
        public string ParamUnit { get; set; }
        /// <summary>
        /// The amount of decimals allowed when registering the parameter's value
        /// </summary>
        public int Decimals { get; set; }
        /// <summary>
        /// Proposed minimum value for this parametertype. Not forced! Lesser values will be allowed. This is a guideline value.
        /// </summary>
        public double Min { get; set; }
        /// <summary>
        /// Proposed maximum value for this parametertype. Not forced! Higher values will be allowed. This is a guideline value.
        /// </summary>
        public double Max { get; set; }
        /// <summary>
        /// Proposed increment value when setting the parameter's value using e.g. a scroll wheel on a keyboardless device. 
        /// E.g. for temperature, the step is 0,1°C.
        /// </summary>
        public double Step { get; set; }
    }

    /// <summary>
    /// Describes a ParamDetail object.
    /// For some parametertypes you can choose to register a secondary value. Chosen from pre-defined, fixed list of parameterdetailvalues.
    /// These lists are fixed in the application. No user details can be created. A single parameter can have 1 secondary value 
    /// (if parameter has a HasDetails:true property) 
    /// E.g. when register a pulse of 75 you can specify if the pulse was 'Normal', 'Weak' or 'Strong' using the ID of the corresponding 
    /// paramdetail as the parameter's secondary value 
    /// </summary>
    public class ParamDetail
    {
        /// <summary>
        /// Unique ID of this ParamDetail
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Unique ID for of the parameter type for which this detail can be registered. (See Parameter object)
        /// </summary>
        public int ParamListID { get; set; }
        /// <summary>
        /// Description/Name of the paramdetail.
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Describes an action/task that user's registered as executed. 
    /// This list contains a tree structure of all possible tasks and their categories available in WZD.
    /// For example: Help dressing a resident, washing a resident, registering a parameter,...
    /// Up until ID 60 this list is fixed and hardcoded in the CS software. However, users can create new, custom tasks and categories
    /// in our software. These items will differ between all installations. You can recognize these actionlist items by the 'isCustomAction'
    /// property.
    /// Keep in mind that this actionlist is a tree view. So the items are grouped into categories by using parent ID’s. The categories headers
    /// do not correspond to a task and thus, cannot be used to register a task. In this case the canRegister property will be false.
    /// </summary>
    public class ActionList
    {
        /// <summary>
        /// Unique ID of the Action. Used when registering a task as executed. 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Description/Name of the task or category
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// True: this actiontype has been custom created by the users.
        /// False: this actiontype has been hardcoded into the software by Care Solutions.
        /// </summary>
        public bool IsCustomAction { get; set; }
        /// <summary>
        /// If the action belongs to a category group, the group's Parent ID is specified using this property
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// Specify if the current session's userID is allowed to register this action.
        /// If the item is a category title, this will be false. As the title of the category is purely informational.
        /// When true, this action can used to register a task.
        /// </summary>
        public bool CanRegister { get; set; }
    }

    /// <summary>
    /// If you create an observation (Entering remarks when registering signing a task/action). You have the option to send 
    /// these remarks to all the different modules used in the CS WZD Software.
    /// E.g. if you notice that a resident has need for incontinence material, while you are performing your daily wash task. 
    /// You can enter this as a remark while signing the wash task. You can then make this remark visible to the users of the
    /// incontinence module, so they pick up on it.
    /// Different end points are possible. (E.g. resident's wounds, ergo, comfort module, diabetes, kine module... etc.)
    /// All these different endpoints are called a 'modlinkable'. The list of modlinkables makes up all endpoints you can send observations to
    /// for any given resident.
    /// This list is a tree with categories using ID's and parent ID's. The Allowlink property defines if this modlinkable is in fact a viable
    /// end point you can send an observation to. If this property is false, the modlinkable is just a category/group node of the tree list.
    /// </summary>
    public class ModLinkable
    {
        /// <summary>
        /// Unique ID of the resident for which this modlinkable is available
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// Unique ID in the tree list.
        /// </summary>
        public string TreeID { get; set; }
        /// <summary>
        /// ID of this node's parent in the tree list
        /// </summary>
        public string ParentTreeID { get; set; }
        /// <summary>
        /// Description/Name of the modlinkable. (E.g. Wound on left shoulder // diabetes // incontinence)
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// ID to use when you want to link. (See method CreateObservation)
        /// </summary>
        public long LinkID { get; set; }
        /// <summary>
        /// If false: you can not use this node as a link.
        /// If true: you can use this node's link ID to link an observation to this endpoint.
        /// </summary>
        public Boolean AllowLink { get; set; }
        /// <summary>
        /// This modlinkable only exists for the resident specified in the ResdidentID property. 
        /// (E.g. an observation for a specific wound of this resident.)
        /// </summary>
        public Boolean IsResidentSpecific { get; set; }
    }

    #endregion

    #region Care Planning
    /// <summary>
    /// Describes an task/action that is planned on this resident's care planning.
    /// These care plan items can be signed as executed or not executed (with reason)
    /// </summary>
    public class CarePlanItem
    {
        /// <summary>
        /// Unique ID for this Care Plan Item.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// This property is obsolete. Will return NULL. Legacy reasons
        /// </summary>
        public int ComfortMaterialID { get; set; }
        /// <summary>
        /// Software module ID in which this task was planned. (Incontinence, Comfort planning, Parameters,....)
        /// </summary>
        public int Module { get; set; }
        /// <summary>
        /// User remarks entered for this care plan item/
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// If this is an informational item, or a calendar item, not planned on a specific caremoment, this property displays the original timestamp
        /// the item was planned on. E.g. If the resident has appointments listed in his/her calendar, users have the option to have these appointments show
        /// on the CarePlanning for the resident during the associated Care Moment. These appointments do not have to be signed, but will appear as an informational
        /// item. If a resident has a doctor visit planned, he/she will not be in his/her room. With this informational item, the staff can immediately see that the resident
        /// is probably at the doctor's office.
        /// </summary>
        public System.DateTime? Hour { get; set; }
        /// <summary>
        /// Description/Name of the task.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Full Name of the resident for which this Careplan item has been planned.
        /// </summary>
        public string Resident { get; set; }
        /// <summary>
        /// Unique ID of the resident for which this Careplan item has been planned.
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// Resident's room-name
        /// </summary>
        public string Room { get; set; }
        /// <summary>
        /// This property signifies whether this task has recently (within the last 7 days) been changed or added.
        /// In our software we mark the changed/added tasks on the planning in red to draw the staff's attention to the new items.
        /// True: item has been changed/added in the last 7 days
        /// False: item has not been changed in the last 7 days.
        /// </summary>
        public bool CarePlanChanged { get; set; }
        /// <summary>
        /// Order in which the care plan item should appear in the list of all Care Plan Items.
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// Unique ID of the planned/associated ActionList object. (See ActionList class)
        /// </summary>
        public int ActionListID { get; set; }
        /// <summary>
        /// If this Care Plan item is a parameter (Actionlist 15) this property contains the Parameter ID associated with this CarePlanItem.
        /// </summary>
        public int? ParamListID { get; set; }
        /// <summary>
        /// Unique link ID. This is the ID that needs to passed back to the CS software when this item is signed/registered as executed/not executed.
        /// This ID is the generated link reference used in our software to Link a planned careplan item to as signed careplan item.
        /// </summary>
        public int LinkTaskID { get; set; }
        /// <summary>
        /// If the Careplan Item has already been signed by a different user (as executed, or as not executed.) This property will contain the unique User ID
        /// of the user that signed the item. (To determine if a task has been registered as executed, you need to use this UserID property in combination with 
        /// the IsNotDone property. (See IsNotDone property)
        /// </summary>
        public int? UserID { get; set; }
        /// <summary>
        /// This item is can only be signed by a nurse. 
        /// </summary>
        public bool IsNurseItem { get; set; }
        /// <summary>
        /// This item is an informational item. This item does not need to registered/signed.
        /// </summary>
        public bool IsInformational { get; set; }
        /// <summary>
        /// The item is signed as not executed. (With reason.)
        /// To determine if an item can be signed:
        /// UserID is null, IsNotDone is null -> Item needs to be signed. (Has not been registered at all)
        /// UserID is set, IsNotDone is true -> Item was signed as not executed. Can be signed as executed by the same or a different user.
        /// UserID is set, IsNotDone is false -> Item was already signed as executed. Can still be signed as executed by a different user. 
        /// 2 resulting executed records will exist in the database.
        /// </summary>
        public bool? IsNotDone { get; set; }
        /// <summary>
        /// The unique ID of the CareMoment this task/action is planned in.
        /// </summary>
        public int CareMomentID { get; set; }
    }
    /// <summary>
    /// The caremoment object describes a time slot used by the CS Software. In WZD, a workday is divided into different shifts. Each day 
    /// starting on the starthour of the first Caremoment. When the last caremoment has passed, a new careday will begin. This is important when
    /// converting real time date times and associating them to a caremoment.
    /// 
    /// Imagine the following caremoment configuration on 10/04/2016:
    /// 
    /// ID      Start hour      Name
    /// 1	    0600            Morning
    /// 2	    1000            Late morning
    /// 3	    1200            Noon
    /// 4	    1400            Afternoon
    /// 5	    1600            Evening
    /// 6	    2000            Late evening
    /// 7	    0000            Midnight
    /// 8	    0400            Early Morning
    /// 
    /// For the application, the day Care Day begins with Caremoment 1 (06:00h). The Care Day runs until Care Moment 8 is passed. (05:59h)
    /// This means that e.g. all tasks that are planned 11/04/2016 At 04:00 will still be part of the tasks planned on real time date 10/04/2016.
    /// For the application a new Care Day starts every morning at 06:00am. So a task planned on real time date 11/04/2016 06:00 will be registered on Care Day 11/04/2016
    /// A task planned on real time date 11/04/2016 05:00 will be registered as a task on Care Day 10/04/2016. 
    /// 
    /// If you need to retrieve the task list for real time date 11/04/2016 02:00h, you need to ask the Care Plan for: Care Day 10/04/2016, CareMoment 6.
    /// 
    /// These CareMoments configuration can differ from customer to customer. When deploying the application we alter these to correspond to the customer's work shift.
    /// There can be a minimum of 2 and a maximum of 8 Caremoments.    
    /// </summary>
    public class CareMoment
    {
        /// <summary>
        /// Unique ID of the CareMoment
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name/Description of the Caremoment.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Start time of the caremoment. Represented as string. E.g. 1000 = 10:00
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// Short name of the caremoment.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Starting hour of the caremoment as string. e.g. 10:00h = 10
        /// </summary>
        public int StartHour { get; set; }

    }
    #endregion CarePlanning

    #region Communication

    /// <summary>
    /// Describes a message of the diary or Day-Night book.
    /// These messages are used by the staff to checkup on the daily affairs, important reminders and status updates of the resident.
    /// </summary>
    [Obsolete("Class is deprecated, use Diary_V2 instead", false)]
    public class Diary
    {
        /// <summary>
        /// Unique ID of the Diary Item
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Unique ID of the resident this Diary Item concerns.
        /// </summary>        
        public int ResidentID { get; set; }
        /// <summary>
        /// Care Date this message was Diary Item for.
        /// </summary>
        public System.DateTime DiaryDate { get; set; }
        /// <summary>
        /// Unique ID of the caremoment this item was registered for.
        /// </summary>
        public int CareMomentID { get; set; }
        /// <summary>
        /// Combination of name of day and caremoment for this Diary Item.
        /// For example: 'Tuesday - Afternoon (14-16)'
        /// </summary>
        public string Day { get; set; }
        /// <summary>
        /// If this Diary Item is an observation that was linked to the diary. The ID of the ActionList task for which this observation was
        /// created is listed here.
        /// E.g. When linking an observation to the diary while taking a resident's pulse, the actionlist ID of the parametertask (15) can be found here.
        /// </summary>
        public int ActionListID { get; set; }
        /// <summary>
        /// The description of the actionlist item will be listed here.
        /// E.g. 'Nurse - Parameters - Pulse' in case of the above example. (ActioListID property)
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Contains the actual message/observation text.
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// UserID of the user who created the Diary Item.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Full Username of the user who created the Diary Item.
        /// </summary>
        public string UserName { get; set; }

    }
    /// <summary>
    /// Describes a message of the diary or Day-Night book. These can be common (non-resident specific) or resident specific.
    /// These messages are used by the staff to checkup on the daily affairs, important reminders and status updates of the resident.
    /// </summary>
    public class Diary_v2
    {
        /// <summary>
        /// Unique ID of the Diary Item
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Unique ID of the resident this diary item concerns. (In case of a resident specific Diary Item.)
        /// 
        /// If this property is null, this diary item is considered common (non-resident specific).
        /// => For the complete care home, or a specific department => see departmentID property.
        /// </summary>       
        public int? ResidentID { get; set; }
        /// <summary>
        /// Care Date this message was Diary Item for.
        /// </summary>
        public System.DateTime DiaryDate { get; set; }
        /// <summary>
        /// Combination of name of day and caremoment for this Diary Item.
        /// For example: 'Tuesday - Afternoon (14-16)'
        /// </summary>
        public int CareMomentID { get; set; }
        /// <summary>
        /// Combination of name of day and caremoment for this Diary Item.
        /// For example: 'Tuesday - Afternoon (14-16)'
        /// </summary>
        public string Day { get; set; }
        /// <summary>
        /// If this Diary Item is an observation that was linked to the diary. The ID of the ActionList task for which this observation was
        /// created is listed here.
        /// E.g. When linking an observation to the diary while taking a resident's pulse, the actionlist ID of the parametertask (15) can be found here.
        /// </summary>
        public int ActionListID { get; set; }
        /// <summary>
        /// The description of the actionlist item will be listed here.
        /// E.g. 'Nurse - Parameters - Pulse' in case of the above example. (ActioListID property)
        /// </summary>  
        public string Description { get; set; }
        /// <summary>
        /// Contains the actual message/observation text.
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// UserID of the user who created the Diary Item.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Full UserName of the user who created the Diary Item.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Department according to resident. Or chosen department if the Diary Item is not resident specific.
        /// </summary>
        public byte? DepartmentID { get; set; }
    }

    /// <summary>
    /// Describes a message sent through the communications module of the CS WZD Software.
    /// This message is the result of an observation that has been linked to a module of the 'IsCommunication' type.
    /// This is specifically used for messages sent to the resident's doctor or family.
    /// </summary>
    public class CommunicationItem
    {
        /// <summary>
        /// Unique ID of the Communication Item
        /// </summary> 
        public int ID { get; set; }
        /// <summary>
        /// Communication module to which the item has been sent.
        /// </summary>
        public int ModuleID { get; set; }
        /// <summary>
        /// The description of the associated actionlist item will be listed here.
        /// E.g. 'Nurse - Parameters - Pulse' in case of the above example. (ActionListID property)
        /// </summary>  
        public string Description { get; set; }
        /// <summary>
        /// Unique ID of the caremoment this item belongs to.
        /// </summary>      
        public int CareMomentID { get; set; }
        /// <summary>
        /// Actual text of the communication item.
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// This indicates that the message was successfully Registered. Should be true. Only used internally in CS software.
        /// </summary>
        public bool Done { get; set; }
        /// <summary>
        /// This property signifies if the item is an observation. Should be true. Only used internally in CS software.
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// UserID that sent/created the communication item.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// If Checked is false, the message is not complete. The user can still append or change the text of the communication item.
        /// Should be true. Only used internally in CS software.
        /// </summary>
        public bool Checked { get; set; }
        /// <summary>
        /// CareDate when the communication item has been created/sent.
        /// </summary>       
        public System.DateTime CommunicationDate { get; set; }
        /// <summary>
        /// Actual date/timestamp when the communication item was created/sent.
        /// </summary>
        public System.DateTime Stamp { get; set; }
        /// <summary>
        /// Resident ID to which the communication item belongs to.
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// True: Communication item is visible in the Care Home's diary. (See GetDiary_v2 method and DiaryItem_V2 class)
        /// False: Communicaiton item is not visible in the Care Home's diary.
        /// </summary>
        public bool Diary { get; set; }
        /// <summary>
        /// The associated ActionListID will be set here.
        /// </summary>
        public int ActionListID { get; set; }
        /// <summary>
        /// If this item was created as an observation for a planned task, the linkID will be the reference to the CarePlan item.
        /// Mostly only used internally in CS software.
        /// </summary>    
        public int Link { get; set; }
        /// <summary>
        /// Communication Item has been read.
        /// </summary>
        public bool ReadByUser { get; set; }
        /// <summary>
        /// Full Username of the user that created/sent the communication item.
        /// </summary>
        public string ReportedByUser { get; set; }
        /// <summary>
        /// Doctor ID of the resident’s linked doctor. Only used internally in CS software. 
        /// </summary>
        public int DoctorID { get; set; }
        /// <summary>
        /// Full name of the resident 
        /// Resident ID
        /// </summary>  
        public string Resident { get; set; }
    }
    /// <summary>
    /// Describes a type of appointment (category) that a user can create in the resident’s calendar.
    /// The list of Appointment types is customizable by the user.
    /// </summary>
    public class AppointmentType
    {
        /// <summary>
        /// Unique ID of the AppointmentType
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name of the AppointmentType E.g.  Manicurist visit, Doctorvisit.
        /// </summary>
        public string Name { get; set; }
    }
    /// <summary>
    /// Describes an appointment in the resident's calendar.
    /// </summary>
    public class Appointment
    {
        /// <summary>
        /// Unique ID of the appointment.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Resident ID of the resident on whose calendar the appointment was created.
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// Uinque ID of this appointment's appointmenttype.
        /// </summary>
        public int AppointmentTypeID { get; set; }
        /// <summary>
        /// Name/description of the appointmenttype
        /// </summary>
        public string AppointmentType { get; set; }
        /// <summary>
        /// Date of the appointment
        /// </summary>
        public System.DateTime AppointmentDate { get; set; }
        /// <summary>
        /// Start time of the appointment
        /// </summary>
        public System.DateTime StartTime { get; set; }
        /// <summary>
        /// End time of the appointment
        /// </summary>
        public System.DateTime EndTime { get; set; }
        /// <summary>
        /// Remarks or comments entered for this appointment
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Specifies whether this appointment is an all-day event
        /// True: appointment is all-day event
        /// False: appointment is not an all-day event.
        /// </summary>
        public bool AllDay { get; set; }
    }
    /// <summary>
    /// Describes a functional module in CS software to which observations can be linked. These modules are also responsible for generating
    /// the list of CarePlan items. e.g. comfort module, wound module, diabetes module, Communication modules... (See modlinkables)
    /// </summary>
    public class Module
    {
        /// <summary>
        /// Unique ID of the module
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name of the module
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// If applicable, this property is used to group the modules with similar functionality.
        /// EG. all communication modules have ModuleGroup set to 1
        /// </summary>
        public int ModuleGroup { get; set; }
    }
    #endregion Communication

    #region Call System
    /// <summary>
    /// This class describes an intervention or emergency call triggered by a resident/room.
    /// CS Software has an option do display the registered calls from a linked third party call system
    /// </summary>
    public class Call
    {
        /// <summary>
        /// Unique identifier of the call
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Unique ID of the room this call was registered for.
        /// </summary>
        public int RoomID { get; set; }
        /// <summary>
        /// Timestamp when the call was opened/created
        /// </summary>
        public System.DateTime Start { get; set; }
        /// <summary>
        /// DateTime when call was answered and someone was present in the room.
        /// </summary>
        public DateTime? Presence { get; set; }
        /// <summary>
        /// Timestamp when call was answered/resolved.
        /// </summary>        
        public System.DateTime Stop { get; set; }
        /// <summary>
        /// Unique ID of the user that has answered/closed the call
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Call's Priority level.
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Description of the call. (Can be entered by the third party call system.)
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Unique ID of the resident in the room that created the call. 
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// Third party's call reference/identifier. (Used in UpdateCall method. ID of the call in the third's parties 
        /// call system)
        /// </summary>
        public string ExternalID { get; set; }
        /// <summary>
        /// CallReasonID (If specified) of the reason/nature registered for this nurse call. Specifies what the call was about. See GetCallReasons method.
        /// </summary>
        public int? CallReasonID { get; set; }
    }
    /// <summary>
    /// This class describes an intervention or emergency call triggered by a resident/room.
    /// CS Software has an option do display the registered calls from a linked third party call system
    /// v2: made start and stopdate nullable to better reflect the db status. Before null dates were returned as 1900-01-01
    /// </summary>
    public class Call_v2
    {
        /// <summary>
        /// Unique identifier of the call
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Unique ID of the room this call was registered for.
        /// </summary>
        public int RoomID { get; set; }
        /// <summary>
        /// Timestamp when the call was opened/created
        /// </summary>
        public System.DateTime? Start { get; set; }
        /// <summary>
        /// DateTime when call was answered and someone was present in the room.
        /// </summary>
        public DateTime? Presence { get; set; }
        /// <summary>
        /// Timestamp when call was answered/resolved.
        /// </summary>        
        public System.DateTime? Stop { get; set; }
        /// <summary>
        /// Unique ID of the user that has answered/closed the call
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Call's Priority level.
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Description of the call. (Can be entered by the third party call system.)
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Unique ID of the resident in the room that created the call. 
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// Third party's call reference/identifier. (Used in UpdateCall method. ID of the call in the third's parties 
        /// call system)
        /// </summary>
        public string ExternalID { get; set; }
        /// <summary>
        /// CallReasonID (If specified) of the reason/nature registered for this nurse call. Specifies what the call was about. See GetCallReasons method.
        /// </summary>
        public int? CallReasonID { get; set; }
    }
    /// <summary>
    /// This class describes a Call reason that can be registered for a nurse call. This reason specifies why the call was made, or what was done during
    /// the call. (Eg. resident was in pain, Resident was hungry) The call reasons in the application are user definable.
    /// </summary>
    public class CallReason
    {
        /// <summary>
        /// Unique ID of the reason. Used while registering a CallReason for a call. (See EditCallForResident)
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The description text of the Reason. Returned in the user session's language.
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// Specifies if a callreason is active or inactive. Only use active callreasons can be used with editcallforresident.
        /// </summary>
        public bool Active { get; set; }
    }
    #endregion call system

    #region Users
    /// <summary>
    /// Describes a user created in the CS software.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique ID of the user
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// User's Last name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User's Firstname
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// User's username. (Referenced as initials in cs software.)
        /// </summary>
        public string Initials { get; set; }
        /// <summary>
        /// User's password. Will be removed in future version
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Obselete, will no longer be used in future version
        /// </summary>
        public string IntelliaKey { get; set; }
        /// <summary>
        /// Signifies whether user account is active or inactive.
        /// True: user is active
        /// False: user is inactive
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Desired language that is configured for this user account. Currently '1' is Dutch, '2' is French
        /// </summary>
        public int LanguageID { get; set; }
        /// <summary>
        /// Defines which user group this user belongs to. Used internally for CS software.
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// User's email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User's badge/dongle/iButton number as used in a third party access system. Can be used by third party applications to reference
        /// the correct CS user. (E.g. Badges or dongles for Room terminals)
        /// </summary>
        public string IButton { get; set; }
        /// <summary>
        /// User's INSZNumbser
        /// </summary>
        public string INSZNumber { get; set; }
        /// <summary>
        /// User's Riziv number. (If applicable. Onlt for Doctors, Kine, Nurse...)
        /// </summary>
        public string RizivNumber { get; set; }
    }
    #endregion users

    #region Medication
    /// <summary>
    /// Decribes a MedicationMoment. CS Software uses pre-defined time-slots in which medication is administered.
    /// A day is divided into different medication moments or 'shifts' when the staff administers different medication to the resident.
    /// Medicationmoments do not use the CareDate principle. They use standard real datetime stamps.
    /// So medication administered on 11/04/2016 02:00hAM will be registered with a date of 11/04/2016.
    /// </summary>    
    public class MedicationMoment
    {
        /// <summary>
        /// Unique ID of the medicationmoment.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// StarTime of the medicationmoment.
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// Order of the medicationmoment. Lower order happens first.
        /// </summary>
        public int Order { get; set; }
    }
    /// <summary>
    /// Describes a form in which medication can exist and be administered. (Ex. drops, solid tablets, cream,....)
    /// </summary>
    public class MedicationAdminForm
    {
        /// <summary>
        /// Unique ID of the medication form.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Description/Display name of the medication form.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// This property sets if the form requires a special action when preparing or administering the medicine.
        /// E.g. drops, injections, creams,...
        /// </summary>
        public bool IsSpecialAction { get; set; }
        /// <summary>
        /// Defines wether this form of medication is stored in an automatic medicine dispenser.
        /// </summary>
        public bool InDispenser { get; set; }
    }
    /// <summary>
    /// Describes a line on the resident's medication chart or scheme. This chart provides an overview of the planned medicine
    /// for a resident over time. This list is built as an overview with parent/child records. E.g. when certain medication is on a special
    /// schedule (alternating time slots...) the planning details and remarks for this medicine can be retrieved using it's
    /// child records. (ParentID and ID property)
    /// </summary>
    public class MedicationSchemeItem
    {
        /// <summary>
        /// Unique ID of the MediciationScheme item
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Each MediciationScheme has a treatment ID. In the application you can plan different medication for a resident. Each
        /// planned medicine is a treatment. Each treatment is linked to a certain medicine. 
        /// The combination of a medicine ID and a Treatment (schedule) make up the planning. This property is the link from the
        /// schema to the planned item in our MSVB software.
        /// </summary>
        public int TreatmentID { get; set; }
        /// <summary>
        /// Unique ID of the MedicationAdminForm this medicine is dispensed in. (Drops, tablets, injection,...)
        /// </summary>
        public Byte AdminFormID { get; set; }
        /// <summary>
        /// If this is a child node in the list, contains e.g. schedule information. The parent node's ID property is set here.
        /// </summary>
        public int? ParentID { get; set; }
        /// <summary>
        /// MedicationSchemeItem's linked Resident ID 
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// Start date of the treatment. (From when should this medicine me administered?)
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// End date of the treatment. (Until when should this medicine me administered?)
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Description/Name of the medicine.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Defines whether this medicine can be administered without a planned schedule, but on a 'if needed' basis. 
        /// (E.g. painkillers,...)
        /// </summary>
        public Boolean Standingorder { get; set; }
        /// <summary>
        /// This medicine is provided by an automatic medicine dispenser.(See InDispenser property of the MedicationAdminForm)
        /// </summary>
        public Boolean ExternallyProvided { get; set; }
        /// <summary>
        /// Contains any remarks entered for this medication line, or any additional info regarding this schedule
        /// </summary>
        public String Remarks { get; set; }
        /// <summary>
        /// Quantity of medicine to be administered in Medication Moment 1 (If applicable)
        /// </summary>
        public Double Posologie1 { get; set; }
        /// <summary>
        /// Quantity of medicine to be administered in Medication Moment 2 (If applicable)
        /// </summary>
        public Double Posologie2 { get; set; }
        /// <summary>
        /// Quantity of medicine to be administered in Medication Moment 3 (If applicable)
        /// </summary>
        public Double Posologie3 { get; set; }
        /// <summary>
        /// Quantity of medicine to be administered in Medication Moment 4 (If applicable)
        /// </summary>
        public Double Posologie4 { get; set; }
        /// <summary>
        /// Quantity of medicine to be administered in Medication Moment 5 (If applicable)
        /// </summary>
        public Double Posologie5 { get; set; }
        /// <summary>
        /// Quantity of medicine to be administered in Medication Moment 6 (If applicable)
        /// </summary>
        public Double Posologie6 { get; set; }
        /// <summary>
        /// Quantity of medicine to be administered in Medication Moment 7 (If applicable)
        /// </summary>
        public Double Posologie7 { get; set; }
        /// <summary>
        /// Quantity of medicine to be administered in Medication Moment 8 (If applicable)
        /// </summary>
        public Double Posologie8 { get; set; }
        /// <summary>
        /// Quantity of medicine to be administered in Medication Moment 9 (If applicable)
        /// </summary>
        public Double Posologie9 { get; set; }
        /// <summary>
        /// Quantity of medicine to be administered in Medication Moment 10 (If applicable)
        /// </summary>
        public Double Posologie10 { get; set; }
        /// <summary>
        /// Quantity of medicine to be administered in Medication Moment 11 (If applicable)
        /// </summary>
        public Double Posologie11 { get; set; }
        /// <summary>
        /// Quantity of medicine to be administered in Medication Moment 12 (If applicable)
        /// </summary>
        public Double Posologie12 { get; set; }
        /// <summary>
        /// Specifies if the medicationscheme Iteme still needs to be validated by a doctor. (If the 'validation is required' option is used in the MSVB applicaiton.
        /// </summary>
        public Boolean ToBeValidatedByDoctor { get; set; }
        /// <summary>
        /// Medication ID of the medicationscheme item.
        /// </summary>
        public int MedicationID { get; set; }
    }
    /// <summary>
    /// Describes a single line on the planned medication list. A medication 'task' that needs to be signed off.
    /// (Used when signing/registering medication as administered)
    /// </summary>
    public class MedicationPlanItem
    {
        /// <summary>
        /// Planned date for the plan item
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// The corresponding treatment ID for which this planned item is generated.  (Used when signing/registering this medication task.)
        /// </summary>
        public int TreatmentID { get; set; }
        /// <summary>
        /// ID of the corresponding MedicationMoment
        /// </summary>
        public Byte MedicationMomentID { get; set; }
        /// <summary>
        /// Unique ID of the medicine that needs to be administered. (Used when signing/registering this medication task.)
        /// </summary>
        public int MedicationID { get; set; }
        /// <summary>
        /// Unique Patient ID in the CS medication application. (Just informational, only used by CS Software)
        /// </summary>
        public int PatientID { get; set; }
        /// <summary>
        /// Unique ID of the Resident that will receive the medicine.
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// CNK number of the medicine.
        /// </summary>
        public int CNK { get; set; }
        /// <summary>
        /// Medication description/name
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Initial dosage to be administered.
        /// </summary>
        public Double DosageInitial { get; set; }
        /// <summary>
        /// Dosages to be administered.
        /// </summary>
        public Double Dosage { get; set; }
        /// <summary>
        /// Defines whether this medication item has been altered or created in the last 7 days. Can be used to draw a user's
        /// attention to new items.
        /// </summary>
        public Boolean Changed { get; set; }
        /// <summary>
        /// Unique ID of the MedicationAdminForm type.
        /// </summary>
        public Byte FormID { get; set; }
        /// <summary>
        /// Unity in which the form is dispensed. (e.g. for drops in cc, for cream in gr,...)
        /// </summary>
        public string Unity { get; set; }
        /// <summary>
        /// The MedicationAdminForm has 'IsSpecial' property set. (See MedicationAdminForm class)
        /// </summary>
        public Boolean Special { get; set; }
        /// <summary>
        /// Indicates if the planned medication action was already signed. If true it is already signed.
        /// </summary>
        public Boolean Done { get; set; }
        /// <summary>
        /// Indicates if the previous action phases were registered? E.g. When planning was requested for Action type 4 (administer medication),
        /// this field indicates if actiontypes 1,2 and 3 were correctly signed. If not, this will be false. You can still sign this action if you want to.
        /// </summary>
        public Boolean PreviousPhaseOk { get; set; }
        /// <summary>
        /// States if this medicine was provided by an automatic medicine dispenser. (Robot, ...) Can differ from the medication's form default.
        /// (See MedicationAdminForm and MedicationSchemeItem)
        /// </summary>
        public Boolean InDispenser { get; set; }
        /// <summary>
        /// Contains any remarks entered for this medication line, or any additional info regarding this planned item
        /// </summary>
        public string Remarks { get; set; }

    }
    /// <summary>
    /// If the care home has special user access/department set. A session's/user different allowed departments can be retrieved.
    /// This class describes a single department a user has access to. (Mostly, not used)
    /// </summary>
    public class MedicationDeptAccess
    {
        /// <summary>
        /// Unique ID of the Department
        /// </summary>
        public Byte DepartmentID { get; set; }
        /// <summary>
        /// Department's name.
        /// </summary>
        public String DepartmentDescription { get; set; }
        /// <summary>
        /// If a default department has been set, and this is the user's default department. This property will be set to true.
        /// </summary>
        public bool? IsDefault { get; set; }
    }
    /// <summary>
    /// Describes a vaccinationtype known in the application. These are the types of vaccines that a resident can have already received,
    /// or that can be given to a resident.
    /// </summary>
    public class VaccinationType
    {
        /// <summary>
        /// Unique ID of the Vaccinationtype.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Description/Name of the vaccinationtype
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Status of the vaccinationtype.
        /// False: this is an inactive vaccinationtype, and is no longer used.
        /// True: This vaccinationtype is active and can be used.
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Default amount of months after which the vaccination expires. (If set, can be null)
        /// </summary>
        public int? MonthsExpiry { get; set; }
    }
    /// <summary>
    /// Describes a standing order that was signed for a resident. E.g. If a standing order was given after the resident requested a painkiller.
    /// A single MedicationStandingOrderLogItem specifies a single medication that was given in time. The combined list of MedicationStandingOrderLogItems
    /// will give an overview of all standing orders given over time.
    /// </summary>
    public class MedicationStandingOrderLogItem
    {
        /// <summary>
        /// Unique ID of the standing order log item.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Unique ID of the resident who received the medication
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// Timestamp when the medication action/standing order was signed.
        /// </summary>
        public DateTime Stamp { get; set; }
        /// <summary>
        /// Timestamp when the medication was actually administered.
        /// </summary>
        public DateTime MedGivenOn { get; set; }
        /// <summary>
        /// User ID of the user who signed the standing order.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Medication ID of the given medication.
        /// </summary>
        public int MedicationID { get; set; }
        /// <summary>
        /// Qty that was administered.
        /// </summary>
        public Double Qty { get; set; }
        /// <summary>
        /// Reason (text) why the medication was administered. (Painkiller when resident had a headache)
        /// </summary>
        public string Reason { get; set; }
    }

    #endregion Medication

    #region wounds
    /// <summary>
    /// List of locations where the injury/wound was received.
    /// E.g. care home, at home, during doctorsvisit,...
    /// </summary>
    public class WoundOrigin
    {
        /// <summary>
        /// Unique ID of the location
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Description/name of the location.
        /// </summary>
        public string Description { get; set; }
    }
    /// <summary>
    /// Defines a wound category. Wounds can be categorized in different categories. (Burn wounds, cuts, decubitus,...)
    /// A woundcategory object defines family/type of wounds that you can use to categorize a resident's wound.
    /// Each woundcategory is treated differently and classified differently (category, treatment and classification options for a wound
    /// are configurable by the user.) For every category a certain set of fields need to be completed when registering a wound. Users can 
    /// configure which WoundClassificationFields need to be/or can be completed when registering, re-evaluating a wound.
    ///
    /// Categories can have a parent/child relation, used for category grouping.
    /// </summary>
    public class WoundCategory
    {
        /// <summary>
        /// Unique ID of the wound category
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// Description/Name of the woundcategory
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// If this property is not null, this property contains the Unique ID of the parent wound category.
        /// </summary>
        public int? ParentID { get; set; }
        /// <summary>
        /// A wound needs to re-evaluated (reclassified) after a certain amount of days to record the evolution of the injury. This property
        /// contains the default amount of days after which a wound of this category needs to re-evaluated.
        /// </summary>
        public int? RemindDays { get; set; }
        /// <summary>
        /// This property contains a list of the wound category's classification fields. These fields make up the different traits that need to be
        /// specified when registering a wound of this category. Some are mandatory, some are optional. (E.g. in case of a cut: Depth of the cut, length of the cut, cut is infected?,...)
        /// </summary>
        public List<WoundCategoryClassificationInfo> ClassificationFields { get; set; }
        /// <summary>
        /// Specifies if the wound category is active in the application. If false, the category is inactive and can not be used to register new
        /// wounds.
        /// </summary>
        public bool? Active { get; set; }
    }
    /// <summary>
    /// Informs about a possible classification’s field that can be registered for a specific wound category. Can be optional or mandatory.
    /// </summary>
    public class WoundCategoryClassificationInfo
    {
        /// <summary>
        /// The fieldname of the WoundClassificationField field. (See WoundClassificationField class)
        /// </summary>
        public string ClassificationFieldName { get; set; }
        /// <summary>
        /// Specifies if the WoundClassificationField is mandatory when registering/classifying a wound. If true: field is required. if false: field is optional.
        /// </summary>
        public bool Mandatory { get; set; }
    }
    /// <summary>
    /// Describes a the actual WoundClassificationField. This class contains info on how to register the classification.
    /// </summary>
    public class WoundClassificationField
    {
        /// <summary>
        /// Fieldname of the WoundClassificationField.
        /// </summary>
        public string ClassificationFieldName { get; set; }
        /// <summary>
        /// Friendly Name/Description of the WoundClassificationField
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Expected data type when registering this WoundClassificationField.
        /// 0: Text (string)
        /// 1: Integer
        /// 2: Decimal value
        /// 5: Date Value
        /// 6: Date Time Value
        /// 14: List item. (selected item ID as integer)
        /// 15: MultiSelect list (selected item ids as array of integer. Each integer is the ID of the selected WoundClassificationFieldValue.)
        /// </summary>
        public int WoundClassificationDataType { get; set; }
        /// <summary>
        /// If the WoundClassificationField is of type 14,15 (Single/Multi Select value list.) This property contains all possible WoundClassificationFieldValues
        /// possible for the WoundClassificationField,
        /// </summary>
        public List<WoundClassificationFieldValue> ValueList { get; set; }
    }
    /// <summary>
    /// Describes a WoundClassificationFieldvalue, used when registering a wound which has WoundClassificationFields of type 14/15.
    /// </summary>
    public class WoundClassificationFieldValue
    {
        /// <summary>
        /// ID of the value. (Used when registering a WoundClassificationField of type 14/15)
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Friendly Name or Description of the value. (E.g. Skin tear WoundClassificationField 'Depth' can be 'deep' (ID 1) or 'shallow' (ID 2) Deep and Shallow are predefined values (set by the user)
        /// in CS Software. When registering a shallow Skin tear’s depth. You used the value 2.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Some WoundClassificationFieldValue allow to pass on a color code as additional value. (E.g., if the wound turns black, or is red from inflammation.
        /// This property describes the color in HEX color-code..
        /// </summary>
        public int? Color { get; set; }
    }
    /// <summary>
    /// Defines a single wound for a resident. One wound can be re-evaluated over time. In the CS Software we refer to this process as classifying a wound.
    /// So, a single resident's wound, can have different classifications over time.
    /// </summary>
    public class ResidentWound
    {
        /// <summary>
        /// Unique ID of the resident wound
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Unique ID of the resident who is injured.
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// In the CS software a user can pinpoint the wound's exact location on a hardcoded JPEG image by specifying X and Y coordinates. There are 2 images:
        /// an image of a person as seen from the front and a second of a person seen from the back. This property defines if the wound is on the front of the body
        /// (first image -> property is true) or on the back of the resident's body. (Property is false.) Using this property, in combination with the X/Y coordinates,
        /// we can accurately display a wound's location on these images.
        /// A Wound's location on the body is spec
        /// </summary>
        public bool FrontOfBody { get; set; }
        /// <summary>
        /// Wound's location X coordinates. (See FrontOfBody property)
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// Wound's location Y coordinates. (See FrontOfBody property)
        /// </summary>
        public float Y { get; set; }
        /// <summary>
        /// Timestamp when the wound was first observed.
        /// </summary>
        public DateTime FirstObservedOn { get; set; }
        /// <summary>
        /// Unique ID of the user that observed the wound for the first time.
        /// </summary>
        public int FirstObservedByUserID { get; set; }
        /// <summary>
        /// Location of the wound, as a string. (The picture mentioned in FrontOfBody property, is divided into zones. The name of the zone is mentioned here)
        /// E.g. 'Left shoulder', 'Right knee',...
        /// </summary>
        public string LocationDescription { get; set; }
        /// <summary>
        /// Unique ID of the wound location
        /// </summary>
        public int LocationID { get; set; }
        /// <summary>
        /// Unique ID of the Wound Origin. (Where resident was injured. E.g. care home, doctor's office... See WoundOrigins.)
        /// </summary>
        public int? OriginID { get; set; }
        /// <summary>
        /// Defines if the wound has been closed/healed. If this property is false, the wound is set as inactive. And is regarded as being healed.
        /// </summary>
        public bool? Closed { get; set; }
        /// <summary>
        /// Unique ID of te user that closed the wound.
        /// </summary>
        public int? ClosedByUserID { get; set; }
        /// <summary>
        /// Timestamp of when the wound was closed.
        /// </summary>
        public DateTime? ClosedOn { get; set; }
        /// <summary>
        /// If available, this Timestamp defines when the wound should be re-evaluated/re-classified to record the wound's evolution.
        /// </summary>
        public DateTime? NextClassificationDate { get; set; }
        /// <summary>
        /// This property contains all the different classifications (evaluations) for this wound. See ResidentWoundClassification class.
        /// </summary>
        public List<ResidentWoundClassification> Classifications { get; set; }
    }
    /// <summary>
    /// Contains a the single classification of a resident's wound. (Describes the details of the wound and its category.)
    /// These classifications are made up of different field values. (ResidentWoundClassificationDetailValue)
    /// (See WoundClassificationFields and WoundClassificationFieldValue classes)
    /// 
    /// Wounds can sometimes change category when they are re-evaluated. 
    /// </summary>
    public class ResidentWoundClassification
    {
        /// <summary>
        /// Unique ID of this ResidentWoundClassification.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ID of this classification's wound category. ('burn’, ‘cut',...)
        /// </summary>
        public int WoundCategoryID { get; set; }
        /// <summary>
        /// When was this ResidentWoundClassification created?
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Unique ID of the user that created this ResidentWoundClassification
        /// </summary>
        public int CreatedByUserID { get; set; }
        /// <summary>
        /// Remarks that were entered when creating this ResidentWoundClassification. (If any)
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// List of classification field name/values set for this classification. (E.g. depth of a cut, degree of inflammation ...)
        /// </summary>
        public List<ResidentWoundClassificationDetailValue> Values { get; set; }
    }
    /// <summary>
    /// ResidentWoundClassificationDetailValue describes a Fieldvalue that was registered when re-evaluating/classifying a wound. (According to the Woundcategory)
    /// </summary>
    public class ResidentWoundClassificationDetailValue
    {
        /// <summary>
        /// Fieldname of the ResidentWoundClassificationDetailValue (See WoundClassificationFieldValue)
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// Value of the ResidentWoundClassificationDetailValue. (See WoundClassificationFieldValue) 
        /// </summary>
        public object Value { get; set; }
    }
    #endregion Wounds

    #region Invoicing
    /// <summary>
    /// Describes an invoice code in the application. These codes are used to invoice a good/service to a resident.
    /// Invoice lines are made up using these codes.
    /// 
    /// This code is used when invoicing a good/service to the resident using the InsertVendorInvoiceLine method.
    /// </summary>
    public class InvoiceCode
    {

        /// <summary>
        /// Unique ID of the invoice code
        /// </summary>
        public Int32 ID { get; set; }
        /// <summary>
        /// Short name/Code of the invoice code. As used to book in the application.
        /// </summary>
        public string InvoiceCodeName { get; set; }
        /// <summary>
        /// Description of the code. As appearing on the resident's invoice.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Specifies if the code is active or inactive. Only active codes can be used to register new goods/services using InsertVendorInvoiceLine
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Code is active until set date. After this code will become inactive.
        /// </summary>
        public DateTime? ActiveUntil { get; set; }
    }
    #endregion Invoicing

    #region WaitingList
    /// <summary>
    /// Describes an entry on the CareHome's waiting list. Contains all resident data and contact data needed for admission.
    /// However, the resident is yet to admitted to the carehome, thus this entry can exist without a corresponding resident object in the application.    
    /// </summary>
    public class WaitingListEntry
    {
        /// <summary>
        /// Unique ID of the waitinglist entry.
        /// </summary>
        public Int32 ID { get; set; }
        /// <summary>
        /// Text that specifies why the resident needs to be / would like to be admitted.
        /// </summary>
        public string ReasonForAdmission { get; set; }
        /// <summary>
        /// Specifies when the resident was enlisted on the institutions waiting list.
        /// </summary>
        public DateTime? EnlistDate { get; set; }
        /// <summary>
        /// Resident's name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Residents firstname
        /// </summary>
        public string Firstname { get; set; }
        /// <summary>
        /// Resident's domicile street and number
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Resident's domicile zip code.
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// Resident's domicile town
        /// </summary>
        public string Town { get; set; }
        /// <summary>
        /// Resident's sex. (false: female, true: male, null: unknown.)
        /// </summary>
        public bool? Sex { get; set; }
        /// <summary>
        /// Resident's date of birth
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Primary phone number
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Name of resident's partner
        /// </summary>
        public string PartnerName { get; set; }
        /// <summary>
        /// Firstname of resident's partner.
        /// </summary>
        public string PartnerFirstName { get; set; }
        /// <summary>
        /// Value describing the residents civil state. This is a LookupValue of Type 3.
        /// </summary>
        public byte? CivilState { get; set; }
        /// <summary>
        /// Indicates if the admission is urgent. (null: unknown)
        /// </summary>
        public bool? AdmissionIsUrgent { get; set; }
        /// <summary>
        /// Describes which type of roon is preffered. (Free text value... used for info only)
        /// </summary>
        public string RoomType { get; set; }
        /// <summary>
        /// Specifies any remarks made during enlistment on the WaitingList.
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// Does this waitinglist entry specifies a shortstay period? (null: unknown)
        /// </summary>
        public bool? IsShortStay { get; set; }
        /// <summary>
        /// If the resident for this entry actually moved to the nursing home, his/her corresponding resident ID as defined in the application 
        /// will be specified here. If this property is null, the resident hasn't been admitted to the nursing home yet.
        /// </summary>
        public int? ResidentID { get; set; }
        /// <summary>
        /// Status of this waitinglist entry. Get different possiblestatusses with GetWaitingListStatusList. Users define these. Different in each carehome.
        /// </summary>
        public int? WaitingListStatus { get; set; }
        /// <summary>
        /// Free text field containt a brief medical anamnesis for the resident.
        /// </summary>
        public string MedicalAnamnesis { get; set; }
        /// <summary>
        /// Defines if the admission is a preventive action.
        /// </summary>
        public bool? AdmissionIsPreventive { get; set; }
        /// <summary>
        /// Resident's domicile country. Specified by country code. (See GetCountryCodes method)
        /// </summary>
        public int? Country { get; set; }
        /// <summary>
        /// Timestamp of creation for this WaitingListEntry
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Last modified date for the WaitingListEntry
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
        /// <summary>
        /// Specifies nationality of the resident. Specified by country code (See GetCountryCodes method)
        /// </summary>
        public int? Nationality { get; set; }
        /// <summary>
        /// Resident's national number
        /// </summary>
        public string NatNumber { get; set; }
        /// <summary>
        /// Resident's email address.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Prferred language for this resident. /// Language ID. Default: 1 is Dutch / 2 French
        /// </summary>
        public byte? PreferredLanguage { get; set; }
        /// <summary>
        /// DateTime when effective admission is planned.
        /// </summary>        
        public DateTime? PlannedAdmissionDate { get; set; }
        /// <summary>
        /// Unique ID of the planned room for this admission . (See GetRoomList method.)
        /// </summary>
        public int? PlannedRoom { get; set; }
        /// <summary>
        /// List of contactpersons for this resident. Currently we support 2 Contacts per waitinglistentry. (See WaitingListContact oject for details.) 
        /// </summary>
        public List<WaitingListContact> Contacts { get; set; }
        /// <summary>
        /// Contains a (last) known Katz Score if it was entered for this WaitingListEntry. (See WaitingListKatzScore object)
        /// </summary>
        public WaitingListKatzScore KatzScore { get; set; }
    }

    /// <summary>
    /// Returns a list of all possible WaitingListStatusses that can be specified in a WaitingListEntry
    /// </summary>
    public class WaitingListStatus
    {
        /// <summary>
        /// Unique ID of the WaitingListStatus
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Description of the waitingliststatus. Retrieved in sessions language.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Defines wether the status is a user created status. Our application is installed with a default list os WaitingListStatusses. However users have the ability to add their own.
        /// They can not delete the system default statusses. So all WaitingListStatusses where IsUserCreatedStatus is false, exist in all our installations.
        /// </summary>
        public bool IsUserCreatedStatus { get; set; }
        /// <summary>
        /// Defines wether this status confirms a user's entry on the current Waitinglist for RVT.
        /// </summary>
        public bool ConfirmedOnRVTList { get; set; }
        /// <summary>
        /// Defines wether this status confirms a user's entry on the current Waitinglist for ShortStay.
        /// </summary>
        public bool ConfirmedOnShortStayList { get; set; }
    }

    /// <summary>
    /// Returns a list of all possible WaitingListStatusses that can be specified in a WaitingListEntry
    /// </summary>
    public class WaitingListContact
    {

        /// <summary>
        /// ID of the WaitingListEntry this contact belongs to. (See WaitingListEntry object)
        /// </summary>
        public int WaitingListEntryID { get; set; }
        /// <summary>
        /// Currently we support only 2 contacts per WaitingListEntry. This value specifies if this contact is #1 or #2
        /// </summary>        
        public int ContactNumber { get; set; }
        /// <summary>
        /// Contact's Name
        /// </summary>        
        public string Name { get; set; }
        /// <summary>
        /// Contact's first name
        /// </summary>    
        public string FirstName { get; set; }
        /// <summary>
        /// Contact's Address
        /// </summary>                
        public string Address { get; set; }
        /// <summary>
        /// Contact's date of birth
        /// </summary>       
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// Contact's domicile country. Specified by country code. (See GetCountryCodes method)
        /// </summary>        
        public int? Country { get; set; }
        /// <summary>
        /// Specifies Nationality of the contact. Specified by country code (See GetCountryCodes method)
        /// </summary>
        public int? Nationality { get; set; }
        /// <summary>
        /// Contact's national number
        /// </summary>
        public string NatNumber { get; set; }
        /// <summary>
        /// Contact's first phone number
        /// </summary>
        public string Phone1 { get; set; }
        /// <summary>
        /// Contact's second phone number
        /// </summary>
        public string Phone2 { get; set; }
        /// <summary>
        /// Prferred language for this contact. /// Language ID. Default: 1 is Dutch / 2 French
        /// </summary>
        public int? PreferredLanguage { get; set; }
        /// <summary>
        /// Preferred communicationmethod for this contact. 
        /// 0: By Letter
        /// 1: By Email
        /// 2: By Phone
        /// </summary>
        public int? PreferredCommunicationMethod { get; set; }
        /// <summary>
        /// Contact's relation to the resident. Specified by a LookupValue. (LookupValue type 87)
        /// </summary>
        public int? RelationToResident { get; set; }
        /// <summary>
        /// Contact's address town
        /// </summary>
        public string Town { get; set; }
        /// <summary>
        /// Contact's address Zip Code
        /// </summary>
        public string ZipCode { get; set; }
    }

    /// <summary>
    /// It is possible to add a (last) known KatzScore to a  WaitingListEntry. (Just a single Katz per entry.)
    /// This Katz is purely informational.
    /// </summary>
    public class WaitingListKatzScore
    {
        /// <summary>
        /// Uniqie ID of this WaitingListKatzScoreObject
        /// </summary>
        public int? ID;
        /// <summary>
        /// ID of the WaitingListEntry onject this Katz Score belongs to.
        /// </summary>
        public int WaitingListEntryID;
        /// <summary>
        /// Timestamp when the Katz was measured.
        /// </summary>
        public DateTime? TimeStamp;
        /// <summary>
        /// Resident's scored Katz for Washing
        /// </summary>
        public int? KatzWash;
        /// <summary>
        /// Resident's scored Katz for Clothing
        /// </summary>
        public int? KatzCloth;
        /// <summary>
        /// Resident's scored Katz for Movement
        /// </summary>
        public int? KatzMove;
        /// <summary>
        /// Resident's scored Katz for Toilet
        /// </summary>
        public int? KatzToilet;
        /// <summary>
        /// Resident's scored Katz for Continence
        /// </summary>
        public int? KatzCont;
        /// <summary>
        /// Resident's scored Katz for Food
        /// </summary>
        public int? KatzFood;
        /// <summary>
        /// Resident's scored Katz for Time awareness
        /// </summary>
        public int? KatzTime;
        /// <summary>
        /// Resident's scored Katz for Location awareness
        /// </summary>
        public int? KatzPlace;
        /// <summary>
        /// Calculated category for this score.
        /// </summary>
        public string KatzCategory;
    }

    #endregion Waitinglist

    #region FallReg
    /// <summary>
    /// This object describes a falling incident that was registered for a resident.    
    /// </summary>
    public class FallIncident
    {
        /// <summary>
        /// Unique ID for this FallRegistration.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Unique ID of the resident who fell.
        /// </summary>
        public int ResidentID { get; set; }
        /// <summary>
        /// User that registered the falling incident.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// CareDate of the incident
        /// </summary>
        public DateTime CareDate { get; set; }
        /// <summary>
        /// CareMoment of the incident
        /// </summary>
        public int CareMomentID { get; set; }
        /// <summary>
        /// LookUpValue ID of Type 31 (-> See GetLookUpValue method) defining the place where the resident fell.
        /// eg. Room, hallway, elevator... When set to ID 9 (other) you can use the text in LocationOther. (Fall was registered using a non standard/listed location)
        /// </summary>
        public int Location{ get; set; }
        /// <summary>
        /// When Location is set to 9 (other)  this property can be used to describe the location where the resident fell.
        /// </summary>
        public string LocationOther{ get; set; }
        /// <summary>
        /// Lookupvalue ID of Type 32 (-> See GetLookUpValue method) defining the nature of the fall. Eg. resident fell out of bed, fel out of chair...
        /// When set to ID 6 (other) you can use the text in NatureOfFallOther. (Fall was registered using a non standard/listed nature)
        /// </summary>
        public int NatureOfFall{ get; set; }
        /// <summary>
        /// When NatureOfFall is set to 6 (other) this property can be used to describe the nature of the fall.
        /// </summary>
        public string NatureOfFallOther{ get; set; }
        /// <summary>
        /// List of LookupValue ID's of type 33 (-> See GetLookUpValue method) defining who was with the resident when he/she fell. 
        /// eg. doctor was present, nurse was present. Multiple persons can be specfied here. When you including 11 (other) you can use the text in
        /// PeoplePresentOther. (Fall was registered using a non standard/listed people present)
        /// </summary>
        public List<int> PeoplePresentOnFall{ get; set; }
        /// <summary>
        /// When PeoplePresentOnFall is set to 6 (other) this property can be used to describe the nature of the fall.
        /// </summary>        
        public string PeoplePresentOnFallOther{ get; set; }
        /// <summary>
        /// If the resident was injured during the incident, this property contains the lookupvalue ID (type 34) of the sustained injury's type.
        /// eg. No Injury (0), No visible injury, Injury severity class 1 slight bruising, mild pain or wounds with little or no care (1),...
        /// </summary>
        public int InjuryByFall{ get; set; }
        /// <summary>
        /// LookupValue of ID type 35 (-> See GetLookupValue method) defining what was the probable cause of the fall (eg. clothing, bad shoes,....)
        /// When set to ID 7 (other) you can use the text in SuspectedCauseOther. (Fall was registered using a non standard/listed SuspectedCause)        
        /// </summary>
        public int SuspectedCause{ get; set; }
        /// <summary>
        /// When SuspectedCause is set to 7 (other) this property can be used to describe the suspected Cause of the fall.
        /// </summary>                
        public string SuspectedCauseOther{ get; set; }
        /// <summary>
        /// Defines if a resident was using freedom restrictive mesures on the moment of the incident. 
        /// (eg. fixated in a wheelchair, bed bars were used to preven falling out of bed,...)
        /// </summary>
        public bool? WasUsingFreedomRestrictedMeasures{ get; set; }
        /// <summary>
        /// Describes which freedom restricted measures were used.
        /// </summary>
        public string FreedomRestrictedMeasureDesc{ get; set; }
        /// <summary>
        /// Timestamp when the incident was created in the database.
        /// </summary>
        public DateTime CreatedOn{ get; set; }
        /// <summary>
        /// Defines if family was informed about the falling incident.
        /// </summary>
        public bool FamilyWasInformed{ get; set; }
        /// <summary>
        /// Defines if the incident is still active
        /// </summary>
        public bool IsActive{ get; set; }
        /// <summary>
        /// defines if the resident's doctor was informed about the falling incident.
        /// </summary>
        public bool DoctorWasInformed{ get; set; }
        /// <summary>
        /// LookupValue of ID type 47 (-> See GetLookupValue method) If a resident has any known medical history that might lead to this falling incident, the nature of this history is defined 
        /// using this property. (eg. In case of Reduced vision, Hearing difficulty, Walking difficulty...)
        /// </summary>
        public int? HistoricalBackground{ get; set; }
        /// <summary>
        /// Timestamp of the actual falling incident.
        /// </summary>
        public DateTime FallTimeStamp{ get; set; }
        /// <summary>
        /// Remarks made when creating the fall incident.
        /// </summary>
        public string Remarks{ get; set; }
    }
    #endregion FallReg

    #region AppNotifications
    /// <summary>
    /// Defines a notification that can be sent and retrieved through the webservice. Eg. an AppNotification can be used to send a popup message to Care Solutions ZappS users.
    /// You can use the for all kinds of messages. Different colors or icons are available and.
    /// Icons are passed by using these: "http://glyphicons.com/"
    /// </summary>
    public class AppNotification
    {
        /// <summary>
        /// Unique system identifier for this AppNotification
        /// </summary>        
        public Guid ID{ get; set; }
        /// <summary>
        /// Notification message. This is the message that will be shown to the users.
        /// </summary>
        public string Text{ get; set; }
        /// <summary>
        /// For icons we use the public http://glyphicons.com/ library. This property let's you choose what icon you want us to show with your AppNotification.
        /// Classname of icon you want to show with the message. If left blank, glyphicons-info-sign 
        /// </summary>
        public string IconClass{ get; set; }
        /// <summary>
        /// Specifies what colorcode will be used when displaying the AppNotification in our Apps.
        /// </summary>
        public string Color{ get; set; }
        /// <summary>
        /// Optional: if you want to link a specific room to this Appnotification, set the corresponding RoomID here.
        /// </summary>
        public int? LinkedRoomID{ get; set; }
        /// <summary>
        /// Optional: if you want to link a specific resident to this AppNotification, set the corresponding LinkedResidentID here.
        /// </summary>
        public int? LinkedResidentID{ get; set; }
        /// <summary>
        /// If you want the AppNotification to only appear for a certain department. You can set the corresponding departmetn ID here.
        /// </summary>
        public List<int> ShowOnlyForDepartmentIDs{ get; set; }
        /// <summary>
        /// Timestamp when this AppNotificationExpires. If the AppNotification is considered expired, it will no longer be kept in the servercache
        /// and the AppNotification will no longer be shown.
        /// </summary>
        public DateTime ExpiresOn{ get; set; }
    }
    #endregion

}
  




