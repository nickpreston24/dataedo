Query below return all stored procedures and information about it in SQL Server database.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> schema_name(obj.schema_id) <span>as</span> schema_name,
       obj.name <span>as</span> procedure_name,
       <span>case</span> <span>type</span>
            <span>when</span> <span>'P'</span> <span>then</span> <span>'SQL Stored Procedure'</span>
            <span>when</span> <span>'X'</span> <span>then</span> <span>'Extended stored procedure'</span>
        <span>end</span> <span>as</span> <span>type</span>,
        <span>substring</span>(par.parameters, <span>0</span>, <span>len</span>(par.parameters)) <span>as</span> <span>parameters</span>,
        mod.definition
<span>from</span> sys.objects obj
<span>join</span> sys.sql_modules <span>mod</span>
     <span>on</span> mod.object_id = obj.object_id
<span>cross</span> <span>apply</span> (<span>select</span> p.name + <span>' '</span> + TYPE_NAME(p.user_type_id) + <span>', '</span> 
             <span>from</span> sys.parameters p
             <span>where</span> p.object_id = obj.object_id 
                   <span>and</span> p.parameter_id != <span>0</span> 
             <span>for</span> <span>xml</span> <span>path</span> (<span>''</span>) ) par (<span>parameters</span>)
<span>where</span> obj.type <span>in</span> (<span>'P'</span>, <span>'X'</span>)
<span>order</span> <span>by</span> schema_name,
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