Query below lists all objects that are referencing to specific object .

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> schema_name(obj.schema_id) +  <span>'.'</span> + obj.name
        + <span>case</span> <span>when</span> referenced_minor_id = <span>0</span> <span>then</span> <span>''</span>
               <span>else</span> <span>'.'</span> + col.name <span>end</span> <span>as</span> referenced_object,
       <span>'referenced by'</span> <span>as</span> <span>'ref'</span>,
       schema_name(ref_obj.schema_id) <span>as</span> referencing_schema,
       ref_obj.name <span>as</span> referencing_object_name,
       <span>case</span> <span>when</span> ref_obj.type_desc = <span>'USER_TABLE'</span> 
                 <span>and</span> dep.referencing_minor_id != <span>0</span>
            <span>then</span> <span>'COLUMN'</span>
            <span>else</span> ref_obj.type_desc <span>end</span> <span>as</span> referencing_object_type,
       ref_col.name <span>as</span> referencing_column
<span>from</span> sys.sql_expression_dependencies dep
<span>join</span> sys.objects obj
     <span>on</span> obj.object_id = dep.referenced_id
<span>left</span> <span>join</span> sys.columns <span>col</span>
     <span>on</span> col.object_id = dep.referenced_id
     <span>and</span> col.column_id = dep.referenced_minor_id
<span>join</span> sys.objects ref_obj
     <span>on</span> ref_obj.object_id = dep.referencing_id
<span>left</span> <span>join</span> sys.columns ref_col
     <span>on</span> ref_col.object_id = dep.referencing_id
     <span>and</span> ref_col.column_id = dep.referencing_minor_id
<span>where</span> schema_name(obj.schema_id) = <span>'Sales'</span>  <span>-- put object schema name here</span>
      <span>and</span> obj.name = <span>'SalesOrderHeader'</span>     <span>-- put object name here</span>
<span>order</span> <span>by</span> referencing_schema,
         referencing_object_name;
```

## Columns

-   **referenced\_object** - provided schema name, object name and eventually automaticlly added column
-   **ref** - string 'referenced by'
-   **referincing\_schema** - schema name of the referencing object
-   **referencing\_object\_name** - name of the referencing object
-   **referencing\_object\_type** - type of the referencing object
-   **referencing\_column** - computed column name

## Rows

-   **One row** represents one depending object
-   **Scope of rows:** all objects that are depending on specified object
-   **Ordered by** referencing object schema name and name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/depending_object.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)