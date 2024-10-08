Query below return all User Defined Functions and objects used by them.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> schema_name(obj.schema_id) <span>as</span> schema_name,
       obj.name <span>as</span> function_name,
       schema_name(dep_obj.schema_id) <span>as</span> referenced_object_schema,
       dep_obj.name <span>as</span> referenced_object_name,
       dep_obj.type_desc <span>as</span> object_type
<span>FROM</span> sys.objects obj
<span>left</span> <span>join</span> sys.sql_expression_dependencies dep
     <span>on</span> dep.referencing_id = obj.object_id
<span>left</span> <span>join</span> sys.objects dep_obj
     <span>on</span> dep_obj.object_id = dep.referenced_id
<span>where</span> obj.type <span>in</span> (<span>'AF'</span>, <span>'FN'</span>, <span>'FS'</span>, <span>'FT'</span>, <span>'IF'</span>, <span>'TF'</span>)
     <span>-- and obj.name = 'function_name'  -- put function name here</span>
<span>order</span> <span>by</span> schema_name,
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