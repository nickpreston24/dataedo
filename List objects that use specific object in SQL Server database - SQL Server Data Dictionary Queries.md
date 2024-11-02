Query below lists all objects that are referencing to specific object .

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select schema_name(obj.schema_id) +  '.' + obj.name
        + case when referenced_minor_id = 0 then ''
               else '.' + col.name end as referenced_object,
       'referenced by' as 'ref',
       schema_name(ref_obj.schema_id) as referencing_schema,
       ref_obj.name as referencing_object_name,
       case when ref_obj.type_desc = 'USER_TABLE' 
                 and dep.referencing_minor_id != 0
            then 'COLUMN'
            else ref_obj.type_desc end as referencing_object_type,
       ref_col.name as referencing_column
from sys.sql_expression_dependencies dep
join sys.objects obj
     on obj.object_id = dep.referenced_id
left join sys.columns col
     on col.object_id = dep.referenced_id
     and col.column_id = dep.referenced_minor_id
join sys.objects ref_obj
     on ref_obj.object_id = dep.referencing_id
left join sys.columns ref_col
     on ref_col.object_id = dep.referencing_id
     and ref_col.column_id = dep.referencing_minor_id
where schema_name(obj.schema_id) = 'Sales'  -- put object schema name here
      and obj.name = 'SalesOrderHeader'     -- put object name here
order by referencing_schema,
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