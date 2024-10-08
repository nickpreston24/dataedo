Query below return all User Defined Functions and information about it in SQL Server database.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> schema_name(obj.schema_id) <span>as</span> schema_name,
       obj.name <span>as</span> function_name,
       <span>case</span> <span>type</span>
            <span>when</span> <span>'FN'</span> <span>then</span> <span>'SQL scalar function'</span>
            <span>when</span> <span>'TF'</span> <span>then</span> <span>'SQL table-valued-function'</span>
            <span>when</span> <span>'IF'</span> <span>then</span> <span>'SQL inline table-valued function'</span>
        <span>end</span> <span>as</span> <span>type</span>,
        <span>substring</span>(par.parameters, <span>0</span>, <span>len</span>(par.parameters)) <span>as</span> <span>parameters</span>,
        TYPE_NAME(ret.user_type_id) <span>as</span> return_type,
        mod.definition
<span>from</span> sys.objects obj
<span>join</span> sys.sql_modules <span>mod</span>
     <span>on</span> mod.object_id = obj.object_id
<span>cross</span> <span>apply</span> (<span>select</span> p.name + <span>' '</span> + TYPE_NAME(p.user_type_id) + <span>', '</span> 
             <span>from</span> sys.parameters p
             <span>where</span> p.object_id = obj.object_id 
                   <span>and</span> p.parameter_id != <span>0</span> 
            <span>for</span> <span>xml</span> <span>path</span> (<span>''</span>) ) par (<span>parameters</span>)
<span>left</span> <span>join</span> sys.parameters ret
          <span>on</span> obj.object_id = ret.object_id
          <span>and</span> ret.parameter_id = <span>0</span>
<span>where</span> obj.type <span>in</span> (<span>'FN'</span>, <span>'TF'</span>, <span>'IF'</span>)
<span>order</span> <span>by</span> schema_name,
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