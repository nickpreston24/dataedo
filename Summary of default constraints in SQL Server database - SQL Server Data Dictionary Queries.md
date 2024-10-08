Query below summarizes default constraints in a database listing all distinct default value definitions for columns with the number of their occurrence.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> 
    con.[definition] <span>as</span> default_definition,
    <span>count</span>(<span>distinct</span> t.object_id) <span>as</span> [<span>tables</span>],
    <span>count</span>(col.column_id) <span>as</span> [<span>columns</span>]
<span>from</span> sys.objects t
    <span>inner</span> <span>join</span> sys.all_columns <span>col</span>
        <span>on</span> col.object_id = t.object_id
    <span>left</span> <span>outer</span> <span>join</span> sys.default_constraints con
        <span>on</span> con.parent_object_id = t.object_id
        <span>and</span> con.parent_column_id = col.column_id
<span>where</span> t.type = <span>'U'</span>
<span>group</span> <span>by</span> con.[definition]
<span>order</span> <span>by</span> [<span>columns</span>] <span>desc</span>, [<span>tables</span>] <span>desc</span>
```

## Columns

-   **default\_definition** - definition of default constraint (formula) and _NULL_ for columns without a default value
-   **tables** - number of tables with this constraint (or number of tables with columns with no constraints for _NULL_ row)
-   **columns** - number of columns with this particular constraint (or no constraint for _NULL_ row)

## Rows

-   **One row** represents one unique definition of default value
-   **Scope of rows:** all distinct definitions of default column values in a database and one NULL row for columns without constraints
-   **Ordered by** the number of occurrences from the most popular

## Sample results

Different default column definitions in AdventureWorks database:

![](https://dataedo.com/asset/img/kb/query/sql-server/default_constraints_summary.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)