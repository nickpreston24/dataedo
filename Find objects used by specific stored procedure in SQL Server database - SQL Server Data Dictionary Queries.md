Query below return all stored procedures and objects used by them.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> schema_name(obj.schema_id) <span>as</span> schema_name,
       obj.name <span>as</span> procedure_name,
       schema_name(dep_obj.schema_id) <span>as</span> referenced_object_schema,
       dep_obj.name <span>as</span> referenced_object_name,
       dep_obj.type_desc <span>as</span> object_type
<span>from</span> sys.objects obj
<span>left</span> <span>join</span> sys.sql_expression_dependencies dep
          <span>on</span> dep.referencing_id = obj.object_id
<span>left</span> <span>join</span> sys.objects dep_obj
          <span>on</span> dep_obj.object_id = dep.referenced_id
<span>where</span> obj.type <span>in</span> (<span>'P'</span>, <span>'X'</span>, <span>'PC'</span>, <span>'RF'</span>)
    <span>--  and obj.name = 'procedure_name'  -- put procedure name here</span>
<span>order</span> <span>by</span> schema_name,
         procedure_name;
```

## Columns

-   **schema\_name** - schema name
-   **procedure\_name** - procedure name
-   **referenced\_object\_schema** - schema name of the referenced object
-   **referenced\_object\_name** - name of the referenced object
-   **object\_type** - type of referenced object

## Rows

-   **One row** represents one referenced object by procedure or only procedure if procedure is not using any objects
-   **Scope of rows:** all objects that are used by procedure in database
-   **Ordered by** schema name and procedure name

## Sample Result

![](https://dataedo.com/asset/img/kb/query/sql-server/procedures_referenced_objects.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)