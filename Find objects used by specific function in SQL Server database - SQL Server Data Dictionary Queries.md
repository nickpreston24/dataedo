Query below return all User Defined Functions and objects used by them.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select schema_name(obj.schema_id) as schema_name,
       obj.name as function_name,
       schema_name(dep_obj.schema_id) as referenced_object_schema,
       dep_obj.name as referenced_object_name,
       dep_obj.type_desc as object_type
FROM sys.objects obj
left join sys.sql_expression_dependencies dep
     on dep.referencing_id = obj.object_id
left join sys.objects dep_obj
     on dep_obj.object_id = dep.referenced_id
where obj.type in ('AF', 'FN', 'FS', 'FT', 'IF', 'TF')
     -- and obj.name = 'function_name'  -- put function name here
order by schema_name,
         function_name;
```

## Columns

-   **schema\_name** - schema name
-   **function\_name** - function name
-   **referenced\_object\_schema** - schema name of the referenced object
-   **referenced\_object\_name** - name of the referenced object
-   **object\_type** - type of referenced object

## Rows

-   **One row** represents one referenced object by function or only function if function is not using any objects
-   **Scope of rows:** all objects that are used by function in database
-   **Ordered by** schema name and function name

## Sample Result

![](https://dataedo.com/asset/img/kb/query/sql-server/functions_referenced_objects.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)