﻿using FleetManagement.Authentication.Hashes;
using FleetManagement.Authentication.Policies;
using FleetManagement.Db.Seeds;
using FleetManagement.Entities.Accounts.DriverAccounts;
using FleetManagement.Entities.Accounts.DriverAccounts.Models;
using FleetManagement.Entities.Accounts.ManagerAccounts;
using FleetManagement.Entities.Accounts.ManagerAccounts.Models;
using FleetManagement.Entities.Accounts.UserAccounts;
using FleetManagement.Entities.Accounts.UserAccounts.Models;
using FleetManagement.Entities.BrandModels;
using FleetManagement.Entities.BrandModels.Models;
using FleetManagement.Entities.Brands;
using FleetManagement.Entities.Brands.Models;
using FleetManagement.Entities.Companies;
using FleetManagement.Entities.Companies.Models;
using FleetManagement.Entities.DriveTypes;
using FleetManagement.Entities.DriveTypes.Models;
using FleetManagement.Entities.EngineTypes;
using FleetManagement.Entities.EngineTypes.Models;
using FleetManagement.Entities.Maintenances;
using FleetManagement.Entities.Maintenances.Models;
using FleetManagement.Entities.Powertrains;
using FleetManagement.Entities.Powertrains.Models;
using FleetManagement.Entities.Refuelings;
using FleetManagement.Entities.Refuelings.Models;
using FleetManagement.Entities.Trips;
using FleetManagement.Entities.Trips.Models;
using FleetManagement.Entities.Vehicles;
using FleetManagement.Entities.Vehicles.Models;
using FleetManagement.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FleetManagement.Controllers
{
    [ApiController]
    [DefaultRoute]
    [AllowAnonymous]
    public class DataBaseController : ControllerBase
    {
        private readonly IHashService hashService;

        private readonly IDbSeeder<IUserAccountProvider, UserAccount> userAccountsSeeder;
        private readonly IDbSeeder<IManagerAccountProvider, ManagerAccount> managerAccountsSeeder;
        private readonly IDbSeeder<IDriverAccountProvider, DriverAccount> driversSeeder;
        private readonly IDbSeeder<IVehicleProvider, Vehicle> vehiclesSeeder;
        private readonly IDbSeeder<IPowertrainProvider, Powertrain> powertrainsSeeder;
        private readonly IDbSeeder<IRefuelingProvider, Refueling> refuelingsSeeder;
        private readonly IDbSeeder<IMaintenanceProvider, Maintenance> maintenancesSeeder;
        private readonly IDbSeeder<ICompanyProvider, Company> companiesSeeder;
        private readonly IDbSeeder<ITripProvider, Trip> tripsSeeder;
        private readonly IDbSeeder<IBrandProvider, Brand> brandsSeeder;
        private readonly IDbSeeder<IBrandModelProvider, BrandModel> brandModelsSeeder;
        private readonly IDbSeeder<IEngineTypeProvider, EngineType> engineTypesSeeder;
        private readonly IDbSeeder<IDriveTypeProvider, DriveType> driveTypesSeeder;

        private readonly List<UserAccount> userAccounts = new List<UserAccount>();
        private readonly List<ManagerAccount> managerAccounts = new List<ManagerAccount>();
        private readonly List<DriverAccount> driverAccounts = new List<DriverAccount>();
        private readonly List<Vehicle> vehicles = new List<Vehicle>();
        private readonly List<Powertrain> powertrains = new List<Powertrain>();
        private readonly List<Refueling> refuelings = new List<Refueling>();
        private readonly List<Maintenance> maintenances = new List<Maintenance>();
        private readonly List<Company> companies = new List<Company>();
        private readonly List<Trip> trips = new List<Trip>();
        private readonly List<Brand> brands = new List<Brand>();
        private readonly List<BrandModel> brandModels = new List<BrandModel>();
        private readonly List<EngineType> engineTypes = new List<EngineType>();
        private readonly List<DriveType> driveTypes = new List<DriveType>();

        public DataBaseController(IHashService hashService,
            IDbSeeder<IUserAccountProvider, UserAccount> usersSeeder,
            IDbSeeder<IManagerAccountProvider, ManagerAccount> managersSeeder,
            IDbSeeder<IDriverAccountProvider, DriverAccount> driversSeeder,
            IDbSeeder<IVehicleProvider, Vehicle> vehiclesSeeder,
            IDbSeeder<IPowertrainProvider, Powertrain> powertrainsSeeder,
            IDbSeeder<IRefuelingProvider, Refueling> refuelingsSeeder,
            IDbSeeder<IMaintenanceProvider, Maintenance> maintenancesSeeder,
            IDbSeeder<ICompanyProvider, Company> companiesSeeder,
            IDbSeeder<ITripProvider, Trip> tripsSeeder,
            IDbSeeder<IBrandProvider, Brand> brandsSeeder,
            IDbSeeder<IBrandModelProvider, BrandModel> brandModelsSeeder,
            IDbSeeder<IEngineTypeProvider, EngineType> engineTypesSeeder,
            IDbSeeder<IDriveTypeProvider, DriveType> driveTypesSeeder)
        {
            this.hashService = hashService;
            this.userAccountsSeeder = usersSeeder;
            this.managerAccountsSeeder = managersSeeder;
            this.driversSeeder = driversSeeder;
            this.vehiclesSeeder = vehiclesSeeder;
            this.powertrainsSeeder = powertrainsSeeder;
            this.refuelingsSeeder = refuelingsSeeder;
            this.maintenancesSeeder = maintenancesSeeder;
            this.companiesSeeder = companiesSeeder;
            this.tripsSeeder = tripsSeeder;
            this.brandsSeeder = brandsSeeder;
            this.brandModelsSeeder = brandModelsSeeder;
            this.engineTypesSeeder = engineTypesSeeder;
            this.driveTypesSeeder = driveTypesSeeder;
        }

        [HttpGet]
        public void Seed()
        {
            userAccounts.AddRange(CreateUserAccounts());
            userAccountsSeeder.Seed(userAccounts);
            
            managerAccounts.AddRange(CreateManagerAccounts());
            managerAccountsSeeder.Seed(managerAccounts);

            refuelings.AddRange(CreateRefuelings());
            refuelingsSeeder.Seed(refuelings);

            maintenances.AddRange(CreateMaintenances());
            maintenancesSeeder.Seed(maintenances);

            trips.AddRange(CreateTrips());
            tripsSeeder.Seed(trips);

            engineTypes.AddRange(CreateEngineTypes());
            engineTypesSeeder.Seed(engineTypes);
            
            driveTypes.AddRange(CreateDriveTypes());
            driveTypesSeeder.Seed(driveTypes);

            powertrains.AddRange(CreatePowertrains());
            powertrainsSeeder.Seed(powertrains);

            brandModels.AddRange(CreateBrandModels());
            brandModelsSeeder.Seed(brandModels);

            brands.AddRange(CreateBrands());
            brandsSeeder.Seed(brands);

            vehicles.AddRange(CreateVehicles());
            vehiclesSeeder.Seed(vehicles);

            driverAccounts.AddRange(CreateDriverAccounts());
            driversSeeder.Seed(driverAccounts);
            
            companies.AddRange(CreateCompanies());
            companiesSeeder.Seed(companies);
        }

        private IEnumerable<UserAccount> CreateUserAccounts()
        {
            Console.WriteLine("db");
            return new List<UserAccount>() 
            { 
                new UserAccount()
                {
                    Id = 1,
                    IsActive = true,
                    FirstName = "Ala",
                    LastName = "Elementarzowa",
                    Email = "admin@poczta.pl",
                    PasswordHash = hashService.GenerateHash("admin"),
                    PhoneNumber = "123456789",
                    Role = Roles.Admin,
                },
                new UserAccount()
                {
                    Id = 2,
                    IsActive = true,
                    FirstName = "Mietek",
                    LastName = "Mietczynski",
                    Email = "mietek@poczta.pl",
                    PasswordHash = hashService.GenerateHash("admin"),
                    PhoneNumber = "987654321",
                    Role = Roles.Driver,
                },
                new UserAccount()
                {
                    Id = 3,
                    IsActive = true,
                    FirstName = "Waldek",
                    LastName = "Waldkowski",
                    Email = "waldek@poczta.pl",
                    PasswordHash = hashService.GenerateHash("admin"),
                    PhoneNumber = "987654321",
                    Role = Roles.Manager,
                },
                new UserAccount()
                {
                    Id = 4,
                    IsActive = true,
                    FirstName = "Stasiek",
                    LastName = "Stasiowski",
                    Email = "stasiek@poczta.pl",
                    PasswordHash = hashService.GenerateHash("admin"),
                    PhoneNumber = "987654321",
                    Role = Roles.Driver,
                },
                new UserAccount()
                {
                    Id = 5,
                    IsActive = true,
                    FirstName = "Robert",
                    LastName = "Kubica",
                    Email = "kubica@poczta.pl",
                    PasswordHash = hashService.GenerateHash("admin"),
                    PhoneNumber = "987654321",
                    Role = Roles.Driver,
                },
                new UserAccount()
                {
                    Id = 6,
                    IsActive = true,
                    FirstName = "Lewis",
                    LastName = "Hamilton",
                    Email = "hamilton@poczta.pl",
                    PasswordHash = hashService.GenerateHash("admin"),
                    PhoneNumber = "123456789",
                    Role = Roles.Driver,
                }
            };   
        }

        private IEnumerable<ManagerAccount> CreateManagerAccounts()
        {
            return new List<ManagerAccount>()
            { 
                new ManagerAccount()
                {
                    UserAccountId = 3,
                }
            };
        }

        private IEnumerable<DriverAccount> CreateDriverAccounts()
        {
            return new List<DriverAccount>()
            {
                new DriverAccount()
                {
                    Id = 1,
                    UserAccountId = 2,
                    DrivingLicenseNumber = "P1234509",
                    Vehicles = vehicles.ToList(),
                },
                new DriverAccount()
                {
                    Id = 2,
                    UserAccountId = 4,
                    DrivingLicenseNumber = "P898989",
                    Vehicles = vehicles.ToList(),
                },
                new DriverAccount()
                {
                    Id = 3,
                    UserAccountId = 5,
                    DrivingLicenseNumber = "P323232",
                    Vehicles = vehicles.ToList(),
                },
                new DriverAccount()
                {
                    Id = 4,
                    UserAccountId = 6,
                    DrivingLicenseNumber = "P111111",
                    Vehicles = new List<Vehicle>(),
                }
            };
        }

        private IEnumerable<Vehicle> CreateVehicles()
        {
            return new List<Vehicle>()
            {
                new Vehicle()
                {
                    IsActive = true,
                    Brand = brands[0],
                    Model = brandModels[0],
                    KmMileage = 180000,
                    LicensePlate = "S0 12345",
                    VIN = "WVWZZZ9NZ12345",
                    YearOfProduction = 2003,
                    TechnicalInspectionDate = new DateTime(2021, 2, 10),
                    Powertrain = powertrains[0],
                    CurbWeight = 1100,
                    InsuranceExpirationDate = new DateTime(2021, 5, 15),
                    RepairsAndServices = new List<Maintenance>(),
                    Refuelings = new List<Refueling>(),
                    Trips = new List<Trip>(),
                },
                new Vehicle()
                {
                    IsActive = true,
                    Brand = brands[0],
                    Model = brandModels[1],
                    KmMileage = 5000,
                    LicensePlate = "W0 54321",
                    VIN = "someVINnumber",
                    YearOfProduction = 2019,
                    TechnicalInspectionDate = new DateTime(2025, 1, 1),
                    Powertrain = powertrains[1],
                    CurbWeight = 1600,
                    InsuranceExpirationDate = new DateTime(2022, 1, 1),
                    RepairsAndServices = maintenances.ToList(),
                    Refuelings = refuelings.ToList(),
                    Trips = trips.ToList(),
                }
            };
        }

        private IEnumerable<Powertrain> CreatePowertrains()
        {
            return new List<Powertrain>()
            {
                new Powertrain()
                {
                    Id = 1,
                    NumberOfCylinders = 4,
                    EngineCapacity = 1898,
                    Horsepower = 101,
                    Torque = 270,
                    EngineType = engineTypes[0],
                    DriveType = driveTypes[1],
                },
                new Powertrain()
                {
                    Id = 2,
                    NumberOfCylinders = 4,
                    EngineCapacity = 2000,
                    Horsepower = 190,
                    Torque = 350,
                    EngineType = engineTypes[2],
                    DriveType = driveTypes[0],
                }
            };
        }

        private IEnumerable<Refueling> CreateRefuelings()
        {
            return new List<Refueling>()
            {
                new Refueling()
                {
                    Id = 1,
                    Time = new DateTime(2012, 12, 12, 12, 12, 12),
                    PlaceDescription = "BP Katowice Rozdzieńskiego",
                    Cost = 5.54,
                    Liters = 1,
                    CostPerLiter = 5.54
                }
            };
        }

        private IEnumerable<Maintenance> CreateMaintenances()
        {
            return new List<Maintenance>()
            {
                new Maintenance()
                {
                    Id = 1,
                    Cost = 199.99,
                    Date = new DateTime(2019, 5, 12),
                    OdometerMileage = 12341,
                    UsedParts = "pompa paliwowa",
                    Description = "to chyba pompa",
                    MaintenanceProviderDescription = "jurek & kiler repairs"
				}
            };
        }

        private IEnumerable<Company> CreateCompanies()
        {
            return new List<Company>()
            {
                new Company()
                {
                    Id = 1,
                    IsActive = true,
                    Name = "JakaśFirma",
                    Description = "Transportujemy rzeczy.",
                    Address = "ul. Miejska 1, Warszawa 89-989",
                    Mail = "firma@poczta.pl",
                    NIP = "123-456-78-90",
                    PhoneNumber = "123456789",
                    Vehicles = vehicles.ToList(),
                    ManagerAccountId = 1,
                    Drivers = new List<DriverAccount>() { driverAccounts[1], driverAccounts[2] }
				},
                new Company()
                {
                    Id = 2,
                    IsActive = true,
                    Name = "Koszmarh",
                    Description = "Kochają studentów.",
                    Address = "Koszmarhowo 13, Kraków 89-989",
                    Mail = "koszmarh@poczta.pl",
                    NIP = "987-65-43-21",
                    PhoneNumber = "123456789",
                    Vehicles = vehicles.ToList(),
                    ManagerAccountId = 1,
                    Drivers = new List<DriverAccount>() { driverAccounts[3] }
				}
            };
        }

        private IEnumerable<Trip> CreateTrips()
        {
            return new List<Trip>()
            {
                new Trip()
                {
                    Id = 1,
                    DriverAccountId = 3,
                    StartPlace = "Katowice",
                    StartTime = new DateTime(2012, 12, 12, 12, 12, 12),
                    DestinationPlace = "Gliwice",
                    DestinationArrivalTime = new DateTime(2012, 12, 12, 12, 28, 12),
                    Distance = 30900,
                    AverageSpeed = 63.2,
                    MaximumSpeed = 90.1,
                    TravelTime = 986,
				},
                new Trip()
                {
                    Id = 2,
                    DriverAccountId = 3,
                    StartPlace = "Katowice",
                    StartTime = new DateTime(2018, 12, 12, 12, 12, 12),
                    DestinationPlace = "Kraków",
                    DestinationArrivalTime = new DateTime(2018, 12, 13, 12, 28, 12),
                    Distance = 92300,
                    AverageSpeed = 89.2,
                    MaximumSpeed = 250.9,
                    TravelTime = 1697,
				},
                new Trip()
                {
                    Id = 3,
                    DriverAccountId = 4,
                    StartPlace = "Warszawa",
                    StartTime = new DateTime(2010, 12, 12, 12, 12, 12),
                    DestinationPlace = "Gdańsk",
                    DestinationArrivalTime = new DateTime(2011, 12, 12, 12, 28, 12),
                    Distance = 12350,
                    AverageSpeed = 63.2,
                    MaximumSpeed = 120.4,
                    TravelTime = 1536,
				}
            };
        }

        private IEnumerable<Brand> CreateBrands()
        {
            return new List<Brand>()
            {
                new Brand()
                {
                    Id = 1,
                    Name = "Volkswagen",
                    Models = new List<BrandModel>() {brandModels[0], brandModels[1]}
                },
                new Brand()
                {
                    Id = 2,
                    Name = "Renault",
                    Models = new List<BrandModel>() {brandModels[4]}
                },
                new Brand()
                {
                    Id = 3,
                    Name = "KIA",
                    Models = new List<BrandModel>() {brandModels[5]}
                },
                new Brand()
                {
                    Id = 4,
                    Name = "Ferrari",
                    Models = new List<BrandModel>() {brandModels[2]}
                },
                new Brand()
                {
                    Id = 5,
                    Name = "Porsche",
                    Models = new List<BrandModel>() {brandModels[3]}
                }
            };
        }

        private IEnumerable<BrandModel> CreateBrandModels()
        {
            return new List<BrandModel>()
            {
                new BrandModel()
                {
                    Id = 1,
                    Name = "Polo"
				},
                new BrandModel()
                {
                    Id = 2,
                    Name = "Passat"
				},
                new BrandModel()
                {
                    Id = 3,
                    Name = "456 Speciale"
				},
                new BrandModel()
                {
                    Id = 4,
                    Name = "911"
				},
                new BrandModel()
                {
                    Id = 5,
                    Name = "Clio"
				},
                new BrandModel()
                {
                    Id = 6,
                    Name = "Cee'd"
				}
            };
        }

        private IEnumerable<EngineType> CreateEngineTypes()
        {
            return new List<EngineType>()
            {
                new EngineType()
                {
                    Id = 1,
                    Name = "Diesel",
				},
                new EngineType()
                {
                    Id = 2,
                    Name = "Hybrid",
				},
                new EngineType()
                {
                    Id = 3,
                    Name = "Petrol",
				},
            };
        }

        private IEnumerable<DriveType> CreateDriveTypes()
        {
            return new List<DriveType>()
            {
                new DriveType()
                {
                    Id = 1,
                    Name = "4WD",
				},
                new DriveType()
                {
                    Id = 1,
                    Name = "FWD",
				},
                new DriveType()
                {
                    Id = 1,
                    Name = "RWD",
				}
            };
        }
    }
}
