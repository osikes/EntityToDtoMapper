EntityToDtoMapper
=================

A framework to make mapping those pesky entity models to dto classes fast and easy.

The intent of this framework is to provide a mechanism via attributes to mark certain properties within a dto class so that the Mapper service will automatically map values from a entity object to the correlating dto property.

Example: 

public class TestDto{

  [MapMe]
  
  public int Id {get;set;}

  public string Name {get;set;}
}

public class Test //this is a entity class

{
  public int Id {get;set;}
}

When calling the service DtoMapper.Map<Test,TestDto>(entityInstance), it will attempt to construct a dto of the specified type, looking for any properties in the passed-in entityInstance, where the property name is the same as the dto, and the dto property has been marked with the MapMe attribute.

In this example, the resulting dto class, would have it's Id property set to whatever the Test's Id value is.

Additonal Functionality:

In many cases, a dto represents some entity, and perhaps some additional relational information, 
Example: You might have a Car entity class, and there might also be a CarModel entity class, where Car might have a foreign key reference to CarModel, 

public class Car {
 
  public int Id {get;set;}
  
  public int CarModelId {get;set;}
 
  public virtual CarModel CarModel {get;set;}
}

public class CarModel {
 
  public int Id {get;set;}
 
  public string CarModelName {get;set;}
 
  public virtual ICollection<Car> Cars{get;set;}
}

You might then have a CarDto where you don't want to represent the entire object hierchy, but it might be useful to have the CarModelName available. You can setup a MapMe attribute to do this!

public class CarDto{
  
  public int Id {get;set;}
  
  public int CarModelId {get;set;}
  
  MapMe("CarModel","CarModelName") //First paramter being the name of the Entity Class, Second being the name of the Property
  
  public string CarModelName {get;set;}
}


If you Dto class has a child dto you can make that with the MapMe as well. The DtoMapper.Map method, will then look at that Dto and it's child properties for any MapMe attributes, and so forth.






