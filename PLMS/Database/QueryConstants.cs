namespace PLMS.Database
{
    class QueryConstants
    {
        public const string InsertAdmin = "Insert Into ADMIN(Name,ID,Password,Email) Values($name,$id,$password,$email)";
        public const string getAdminDetails = "Select Name,Password From Admin;";
        public const string InsertParkedVehicle = "Insert into ParkedVehicle(VehicleType,VehicleNumber,ParkingSpot,Floor) Values($vehicletype,$vehiclenumber,$parkingspot,$Floor);";
        public const string InsertVehicle = "Insert into VehicleDetails(VehicleType,VehicleNumber,VehicleInDateTime,VehicleOutDateTime,TotalTimeDuration,IsParked,ParkingFee) Values($vehicletype,$vehiclenumber,$vehicleindatetime,$vehicleoutdatetime,$totaltimeduration,$isParked,$parkingFee);";
        public const string UpdateVehicleDetails = "Update VehicleDetails set VehicleOutDateTime = $vehicleoutdatetime Where VehicleNumber=$vehicleNumber and IsParked=1;";
        public const string CalculateTimeDuration = "SELECT TotalTimeDuration, ROUND((JULIANDAY(VehicleOutDateTime) - JULIANDAY(VehicleInDateTime))*24,2) AS difference FROM VehicleDetails Where VehicleNumber = $vehicleNumber and IsParked=1; ";
        public const string UpdateTimeDuration = "Update VehicleDetails set TotalTimeDuration = ROUND($totaltimeduration,2),IsParked=$isParked  Where VehicleNumber=$vehicleNumber and IsParked=1;";
        public const string GetParkingDuration = "Select TotalTimeDuration From VehicleDetails Where VehicleNumber=$vehicleNumber Order by VehicleIndateTime   Desc limit 1;";       
        public const string InsertNewFloor = "Insert into ParkingSpace(BikeSpace,CarSpace,BusSpace) Values($bikespace,$carspace,$busspace);";
        public const string IncreaseBikeParkingSpace = "Update ParkingSpace Set BikeSpace = BikeSpace+$space where Floor = $floor";
        public const string IncreaseCarParkingSpace = "Update ParkingSpace Set CarSpace = CarSpace+$space where Floor = $floor";
        public const string IncreaseBusParkingSpace = "Update ParkingSpace Set BusSpace = BusSpace+$space where Floor = $floor";
        public const string DecreaseBikeParkingSpace = "Update ParkingSpace Set BikeSpace = BikeSpace-$space where Floor = $floor";
        public const string DecreaseCarParkingSpace = "Update ParkingSpace Set CarSpace = CarSpace-$space where Floor = $floor";
        public const string DecreaseBusParkingSpace = "Update ParkingSpace Set BusSpace = BusSpace-$space where Floor = $floor";
        public const string DeleteVehicleFromVehicleDetails = "Delete From VehicleDetails Where VehicleNumber=$vehicleNumber Order By VehicleIndateTime   Desc limit 1; ";
        public const string RemoveVehicleFromParkedVehicle = "Delete From ParkedVehicle where VehicleNumber=$vehicleNumber;";
        public const string GetFeeStructureDetails = "Select ChargeForFirstHour,ChargeAfterFirstHour From ParkingCharges Where VehicleType = $vehicleType ;";
        public const string UpdateParkingFee = "Update VehicleDetails set ParkingFee = ROUND($parkingFee,2) Where VehicleNumber=$vehicleNumber Order By VehicleIndateTime   Desc limit 1;";
        public const string GetParkingSpaceDetails = "Select * From ParkingSpace Where Floor = $floor;";
        public const string InsertParkingSpotDetails = "Insert into ParkingSpots(Floor,ParkingSpotNumber,ParkingSpotCategory,IsOccupied) Values($floor,$parkingspotnumber,$parkingspotcategory,$isoccupied);";
        public const string CheckForVehicleInDB = "Select * From ParkedVehicle Where VehicleNumber =$vehicleNumber;";
        public const string AllotParkingSpot = "Select * from ParkingSpots Where ParkingSpotCategory=$parkingspotcategory;";
        public const string AllotDifferentParkingSpotForBike = "Select * from ParkingSpots Where ParkingSpotCategory!=$parkingspotcategory;";
        public const string AllotDifferentParkingSpotForCar = "Select * from ParkingSpots Where ParkingSpotCategory=$parkingspotcategory;";
        public const string UpdateParkingSpot = "Update ParkingSpots set IsOccupied=$isOccupied Where Floor =$floor AND ParkingSpotNumber=$parkingSpotNumber; ";
        public const string GetParkedVehicleDetails = "Select * From ParkedVehicle Where VehicleNumber = $vehicleNumber;";
        public const string GetParkingSpotDetails = "Select * From ParkingSpots;";
        public const string InsertInFeeStructure = "Insert Into ParkingCharges (VehicleType,ChargeForFirstHour,ChargeAfterFirstHour) Values($vehicleType,$forfirsthour,$afterfirsthour);";
        public const string GetCurrentFloor = "Select Floor From ParkingSpace Order By Floor Desc limit 1;";
        public const string GetParkingSpotCategoryFromDB = "Select ParkingSpotCategory From ParkingSpots where Floor = $floor and  ParkingSpotNumber=$parkingSpotNumber;";
        public const string CheckForEmptySpots = "Select * From ParkingSpots where IsOccupied=0;";


    }
}
