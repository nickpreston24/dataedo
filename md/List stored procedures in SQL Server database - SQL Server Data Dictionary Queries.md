Query below return all stored procedures and information about it in SQL Server database.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select schema_name(obj.schema_id) as schema_name,
       obj.name as procedure_name,
       case type
            when 'P' then 'SQL Stored Procedure'
            when 'X' then 'Extended stored procedure'
        end as type,
        substring(par.parameters, 0, len(par.parameters)) as parameters,
        mod.definition
from sys.objects obj
join sys.sql_modules mod
     on mod.object_id = obj.object_id
cross apply (select p.name + ' ' + TYPE_NAME(p.user_type_id) + ', ' 
             from sys.parameters p
             where p.object_id = obj.object_id 
                   and p.parameter_id != 0 
             for xml path ('') ) par (parameters)
where obj.type in ('P', 'X')
order by schema_name,
         procedure_name;
```

## Columns

-   **schema\_name** - name of the database (schema)
-   **procedure\_name** - name of the function/procedure
-   **type** - type of procedure
    -   SQL Stored Procedure
    -   Extended stored procedure
-   **parameters** - name of parameters with their data type separated by comma ','
-   **definition** - text of the SQL statement executed by the function/procedure

## Rows

-   **One row** - represents one procedure
-   **Scope of rows:** - all procedures in database
-   **Ordered by** - schema name and procedure name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/stored_procedures.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)