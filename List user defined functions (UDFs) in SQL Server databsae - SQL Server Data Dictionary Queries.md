Query below return all User Defined Functions and information about it in SQL Server database.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(obj.schema_id) as schema_name,
       obj.name as function_name,
       case type
            when 'FN' then 'SQL scalar function'
            when 'TF' then 'SQL table-valued-function'
            when 'IF' then 'SQL inline table-valued function'
        end as type,
        substring(par.parameters, 0, len(par.parameters)) as parameters,
        TYPE_NAME(ret.user_type_id) as return_type,
        mod.definition
from sys.objects obj
join sys.sql_modules mod
     on mod.object_id = obj.object_id
cross apply (select p.name + ' ' + TYPE_NAME(p.user_type_id) + ', ' 
             from sys.parameters p
             where p.object_id = obj.object_id 
                   and p.parameter_id != 0 
            for xml path ('') ) par (parameters)
left join sys.parameters ret
          on obj.object_id = ret.object_id
          and ret.parameter_id = 0
where obj.type in ('FN', 'TF', 'IF')
order by schema_name,
         function_name;
```

## Columns

-   **schema\_name** - schema name
-   **function\_name** - function name
-   **type** - type of UDF:
    -   SQL scalar function
    -   SQL inline table-valued function
    -   SQL table-valued-function
-   **parameters** - name of parameters and their data types
-   **return\_type** - for scalar function data type of returning scalar value
-   **definition** - function definition (CREATE FUNCTION statement)

## Rows

-   **One row** represents one function
-   **Scope of rows:** all user defined functions in database
-   **Ordered by** schema name and function name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/user-functions.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)