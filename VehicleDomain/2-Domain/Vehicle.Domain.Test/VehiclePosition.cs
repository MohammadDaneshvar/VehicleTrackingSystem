//using AppService.Config;
//using Framework.Application.Config;
//using Framework.Domain.Repository;
//using GeoCoordinatePortable;
//using Moq;
//using NetTopologySuite.Geometries;
//using NUnit.Framework;
//using SimpleInjector;
//using System.Collections.Generic;
//using Vehicle.AppService.Command_Handler.Subscriptions;
//using Vehicle.Domain.DynamicProperties;
//using Vehicle.Domain.PropertyTypes;
//using Vehicle.Domain.VehiclePositions;
//using Vehicle.Domain.VehicleProperties;

//namespace Vehicle.Domain.Test
//{
//    public class Tests
//    {
//        public static Container _container;
//        [SetUp]
//        public void Setup()
//        {
//            _container = new Container();
//            FrameworkConfigurator.WireUp(_container, false, typeof(VehicleAppService).Assembly, typeof(AppServiceConfigurator).Assembly);
//            AppServiceConfigurator.WireUp(_container);
//        }

//        [Test]
//        public void AddProperty()
//        {


//            //var validator = new FeatureValidatorMoc();
//            var moq = new Moq.Mock<IPropertyValidator>();
//            moq.Setup(s => s.PropertyIsDuplication(It.IsAny<Property>())).Returns(true);

//            var validator = moq.Object;

            
            
//            var fuelType = new TextType(1, false);
//            var SpeedType = new SelectiveType(2, false, new List<Option>
//            {
//            new Option("0"),
//            new Option("50"),
//            new Option("100"),
//            });

//            var repositoryProperty = _container.GetInstance<IRepository<Property>>();

//            var fuel = new Property(1, "Fuel", new PropertyValidator(repositoryProperty));
//            var speed = new Property(2, "Speed", new PropertyValidator(repositoryProperty));

            
//            var propertyValues = new List<PropertyValue>
//            {
//               fuelType.CreatePropertyValue("80"),
//            SpeedType.CreatePropertyValue("80"),
//        };
//            var VehiclePosition = new VehiclePosition(1, new Point(22, 22), propertyValues);
//            Assert.Pass();
//        }

//        [Test]
//        public void GetPublicKey()
//        {

//        }
//    }
//}