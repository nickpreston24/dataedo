Query below list objects where specific table or view is used.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select schema_name(o.schema_id) + '.' + o.name as [table],
       'is used by' as ref,
       schema_name(ref_o.schema_id) + '.' + ref_o.name as [object],
       ref_o.type_desc as object_type
from sys.objects o
join sys.sql_expression_dependencies dep
     on o.object_id = dep.referenced_id
join sys.objects ref_o
     on dep.referencing_id = ref_o.object_id
where o.type in ('V', 'U')
      and schema_name(o.schema_id) = 'Person'  -- put schema name here
      and o.name = 'Person'   -- put table/view name here
order by [object]
```

## Columns

-   **table** - provided table/view schema name and name
-   **ref** - string 'is used by'
-   **object** - name of object with schema name which use specific procedure
-   **object\_type** - type of found object

## Rows

-   **One row** represents one object
-   **Scope of rows:** all objects that are using provided table/view
-   **Ordered by** found object schema name and name

## Sample Results

List of objects that are using **Person.Person** table in **AdventureWorks2017** database.

![](https://dataedo.com/asset/img/kb/query/sql-server/find_where_table_used.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)