Query below list objects where specific procedure is used.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> schema_name(o.schema_id) + <span>'.'</span> + o.name <span>as</span> [<span>procedure</span>],
       <span>'is used by'</span> <span>as</span> <span>ref</span>,
       schema_name(ref_o.schema_id) + <span>'.'</span> + ref_o.name <span>as</span> [<span>object</span>],
       ref_o.type_desc <span>as</span> object_type
<span>from</span> sys.objects o
<span>join</span> sys.sql_expression_dependencies dep
     <span>on</span> o.object_id = dep.referenced_id
<span>join</span> sys.objects ref_o
     <span>on</span> dep.referencing_id = ref_o.object_id
<span>where</span> o.type <span>in</span> (<span>'P'</span>, <span>'X'</span>)
      <span>and</span> schema_name(o.schema_id) = <span>'dbo'</span>  <span>-- put schema name here</span>
      <span>and</span> o.name = <span>'uspPrintError'</span>  <span>-- put procedure name here</span>
<span>order</span> <span>by</span> [<span>object</span>];
```

## Columns

-   **procedure** - provided procedure schema name and name
-   **ref** - string 'is used by'
-   **object** - name of object with schema name which use specific procedure
-   **object\_type** - type of found object

## Rows

-   **One row** represents one object
-   **Scope of rows:** all objects that are using provided procedure
-   **Ordered by** object schema name and name

## Sample Results

List of objects that are using **dbo.uspPrintError** procedure in **AdventureWorks2017** database. ![](https://dataedo.com/asset/img/kb/query/sql-server/find_where_procedure_used.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)