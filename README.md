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


