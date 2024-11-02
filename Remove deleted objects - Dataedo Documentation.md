Applies to: **Dataedo 24.x (current)** versions, Article available also for: [10.x](https://dataedo.com/docs-10/remove-deleted-objects), [23.x](https://dataedo.com/docs-23/remove-deleted-objects)

Finding all removed objects after major database changes can be tedious.

To make it easier for you, we've created a script which lists all objects marked as removed and generates delete statements you can run straight on the repository.

## What you need

1.  Write access to Dataedo repository database
2.  Database console like SSMS

## Steps

1.  Paste script below to database console (SSMS)
2.  Replace **'Your\_documentation'** with your documentation name (or a name pattern) and run the script below.
3.  Run script. Don't worry, nothing got deleted yet.
4.  Results will show you deleted objects
5.  Copy last column and paste to query editor (this is a set of queries that will remove unnecessary objects, see screen below).
6.  Execute script you copied from the result

![Results of the script](https://dataedo.com/asset/img/docs/6_0/remove_deleted_result.png)

```
--pattern for documentation name
-- set to '%' to find removed from all documentations
DECLARE @doc_name NVARCHAR(1024) = 'Your_documentation';

SELECT docs.[title] AS [documentation]
    , 'constraint column' AS [object type]
    , CONCAT (
        tabs.[schema]
        , '.'
        , cons.[name]
        , '.'
        , cols.[name]
        ) AS [object name]
    , CONCAT (
        'delete from [dbo].[unique_constraints_columns] where status=''D'' and unique_constraint_column_id='
        , concols.unique_constraint_column_id
        ) AS [delete command]
FROM [dbo].[unique_constraints_columns] concols
INNER JOIN [dbo].[unique_constraints] cons
    ON concols.[unique_constraint_id] = cons.[unique_constraint_id]
INNER JOIN [dbo].[columns] cols
    ON concols.[column_id] = cols.[column_id]
INNER JOIN [dbo].[tables] tabs
    ON cols.[table_id] = tabs.[table_id]
INNER JOIN [dbo].[databases] docs
    ON docs.[database_id] = tabs.[database_id]
WHERE concols.[STATUS] = 'D'
    AND cols.[STATUS] &lt;&gt; 'D'
    AND tabs.[STATUS] &lt;&gt; 'D'
    AND cons.[STATUS] &lt;&gt; 'D'
    AND docs.[title] LIKE @doc_name

UNION ALL

SELECT docs.[title] AS [documentation]
    , 'constraint' AS [object type]
    , CONCAT (
        tabs.[schema]
        , '.'
        , cons.[name]
        ) AS [object name]
    , CONCAT (
        'delete from [dbo].[unique_constraints] where status=''D'' and unique_constraint_id='
        , cons.[unique_constraint_id]
        ) AS [delete command]
FROM [dbo].[unique_constraints] cons
INNER JOIN [dbo].[tables] tabs
    ON cons.table_id = tabs.table_id
INNER JOIN [dbo].[databases] docs
    ON docs.[database_id] = tabs.[database_id]
WHERE cons.[status] = 'D'
    AND tabs.[status] &lt;&gt; 'D'
    AND docs.[title] LIKE @doc_name

UNION ALL

SELECT docs.[title] AS [documentation]
    , 'trigger' AS [object type]
    , CONCAT (
        tabs.[schema]
        , '.'
        , tabs.[name]
        , '.'
        , trgs.[name]
        ) AS [object name]
    , CONCAT (
        'delete from [dbo].[triggers] where status=''D'' and trigger_id='
        , trgs.[trigger_id]
        ) AS [delete command]
FROM [dbo].[triggers] trgs
INNER JOIN [dbo].[tables] tabs
    ON trgs.[table_id] = tabs.[table_id]
INNER JOIN [dbo].[databases] docs
    ON docs.[database_id] = tabs.[database_id]
WHERE trgs.[status] = 'D'
    AND tabs.[status] &lt;&gt; 'D'
    AND docs.[title] LIKE @doc_name

UNION ALL

SELECT docs.[title] AS [documentation]
    , 'relation column' AS [object type]
    , CONCAT (
        rels.[name]
        , ': '
        , pk_tabs.[name]
        , '.'
        , pk_cols.[name]
        , ' to '
        , fk_cols.[name]
        , '.'
        , fk_cols.[name]
        ) AS [object name]
    , CONCAT (
        'delete from [dbo].[tables_relations_columns] where status=''D'' and unique_constraint_column_id='
        , relcols.table_relation_column_id
        ) AS [delete command]
FROM [dbo].[tables_relations_columns] relcols
INNER JOIN [dbo].[tables_relations] rels
    ON relcols.table_relation_id = rels.table_relation_id
INNER JOIN [dbo].[tables] fk_tabs
    ON rels.fk_table_id = fk_tabs.table_id
INNER JOIN [dbo].[tables] pk_tabs
    ON rels.pk_table_id = pk_tabs.table_id
INNER JOIN [dbo].[columns] fk_cols
    ON fk_cols.column_id = relcols.column_fk_id
INNER JOIN [dbo].[columns] pk_cols
    ON pk_cols.column_id = relcols.column_pk_id
INNER JOIN [dbo].[databases] docs
    ON (
            pk_tabs.database_id = docs.[database_id]
            OR fk_tabs.database_id = docs.[database_id]
            )
WHERE relcols.[status] = 'D'
    AND rels.[status] &lt;&gt; 'D'
    AND pk_tabs.[status] &lt;&gt; 'D'
    AND fk_tabs.[status] &lt;&gt; 'D'
    AND docs.[title] LIKE @doc_name

UNION ALL

SELECT docs.[title] AS [documentation]
    , 'relation' AS [object type]
    , rels.[name] AS [object name]
    , CONCAT (
        'delete from [dbo].[erd_links] where relation_id='
        , rels.[table_relation_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[tables_relations] where status=''D'' and table_relation_id='
        , rels.[table_relation_id]
        ) AS [delete command]
FROM [dbo].[tables_relations] rels
INNER JOIN [dbo].[tables] fk_tabs
    ON rels.[fk_table_id] = fk_tabs.[table_id]
INNER JOIN [dbo].[tables] pk_tabs
    ON rels.[pk_table_id] = pk_tabs.[table_id]
INNER JOIN [dbo].[databases] docs
    ON (
            pk_tabs.database_id = docs.[database_id]
            OR fk_tabs.database_id = docs.[database_id]
            )
WHERE rels.[status] = 'D'
    AND docs.[title] LIKE @doc_name

UNION ALL

SELECT docs.[title] AS [documentation]
    , 'procedure parameter' AS [object type]
    , CONCAT (
        procs.[name]
        , '.'
        , pars.[name]
        ) AS [object name]
    , CONCAT (
        'delete from [dbo].[parameters] where status=''D'' and parameter_id='
        , pars.[parameter_id]
        ) AS [delete command]
FROM [dbo].[parameters] pars
INNER JOIN [dbo].[procedures] procs
    ON procs.[procedure_id] = pars.[procedure_id]
INNER JOIN [dbo].[databases] docs
    ON docs.[database_id] = procs.[database_id]
WHERE pars.[status] = 'D'
    AND procs.[status] &lt;&gt; 'D'
    AND procs.[object_type] = 'PROCEDURE'
    AND docs.[title] LIKE @doc_name

UNION ALL

SELECT docs.[title] AS [documentation]
    , 'function parameter' AS [object type]
    , CONCAT (
        procs.[name]
        , '.'
        , pars.[name]
        ) AS [object name]
    , CONCAT (
        'delete from [dbo].[parameters] where status=''D'' and parameter_id='
        , pars.[parameter_id]
        ) AS [delete command]
FROM [dbo].[parameters] pars
INNER JOIN [dbo].[procedures] procs
    ON procs.[procedure_id] = pars.[procedure_id]
INNER JOIN [dbo].[databases] docs
    ON docs.[database_id] = procs.[database_id]
WHERE pars.[status] = 'D'
    AND procs.[status] &lt;&gt; 'D'
    AND procs.[object_type] = 'FUNCTION'
    AND docs.[title] LIKE @doc_name

UNION ALL

SELECT docs.[title] AS [documentation]
    , 'procedure' AS [object type]
    , procs.[name] AS [object name]
    , CONCAT (
        'delete from [dbo].[procedures_modules] where procedure_id='
        , procs.[procedure_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[procedures] where status=''D'' and procedure_id='
        , procs.[procedure_id]
        ) AS [delete command]
FROM [dbo].[procedures] procs
INNER JOIN [dbo].[databases] docs
    ON docs.[database_id] = procs.[database_id]
WHERE STATUS = 'D'
    AND procs.[object_type] = 'PROCEDURE'
    AND docs.[title] LIKE @doc_name

UNION ALL

SELECT docs.[title] AS [documentation]
    , 'function' AS [object type]
    , procs.[name] AS [object name]
    , CONCAT (
        'delete from [dbo].[procedures_modules] where procedure_id='
        , procs.[procedure_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[procedures] where status=''D'' and procedure_id='
        , procs.[procedure_id]
        ) AS [delete command]
FROM [dbo].[procedures] procs
INNER JOIN [dbo].[databases] docs
    ON docs.[database_id] = procs.[database_id]
WHERE STATUS = 'D'
    AND procs.[object_type] = 'FUNCTION'
    AND docs.[title] LIKE @doc_name

UNION ALL

SELECT docs.[title] AS [documentation]
    , 'view column' AS [object type]
    , CONCAT (
        tabs.[schema]
        , '.'
        , tabs.[name]
        , '.'
        , cols.[name]
        ) AS [object name]
    , CONCAT (
        'delete from [dbo].[erd_nodes_columns] where column_id='
        , cols.[column_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[unique_constraints_columns] where column_id='
        , cols.[column_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[tables_relations_columns] where column_pk_id='
        , cols.[column_id]
        , ' OR column_fk_id='
        , cols.[column_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[columns] where status=''D'' and column_id='
        , cols.[column_id]
        ) AS [delete command]
FROM [dbo].[columns] cols
INNER JOIN [dbo].[tables] tabs
    ON tabs.[table_id] = cols.[table_id]
INNER JOIN [dbo].[databases] docs
    ON docs.[database_id] = tabs.[database_id]
WHERE cols.[status] = 'D'
    AND tabs.[status] &lt;&gt; 'D'
    AND tabs.[object_type] = 'VIEW'
    AND docs.[title] LIKE @doc_name

UNION ALL

SELECT docs.[title] AS [documentation]
    , 'table column' AS [object type]
    , CONCAT (
        tabs.[schema]
        , '.'
        , tabs.[name]
        , '.'
        , cols.[name]
        ) AS [object name]
    , CONCAT (
        'delete from [dbo].[erd_nodes_columns] where column_id='
        , cols.[column_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[unique_constraints_columns] where column_id='
        , cols.[column_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[tables_relations_columns] where column_pk_id='
        , cols.[column_id]
        , ' OR column_fk_id='
        , cols.[column_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[columns] where status=''D'' and column_id='
        , cols.[column_id]
        ) AS [delete command]
FROM [dbo].[columns] cols
INNER JOIN [dbo].[tables] tabs
    ON tabs.[table_id] = cols.[table_id]
INNER JOIN [dbo].[databases] docs
    ON docs.[database_id] = tabs.[database_id]
WHERE cols.[status] = 'D'
    AND tabs.[status] &lt;&gt; 'D'
    AND tabs.[object_type] = 'TABLE'
    AND docs.[title] LIKE @doc_name

UNION ALL

SELECT docs.[title] AS [documentation]
    , 'view' AS [object type]
    , CONCAT (
        tabs.[schema]
        , '.'
        , tabs.[name]
        ) AS [object name]
    , CONCAT (
        'delete from [dbo].[erd_nodes_columns] where column_id in (select column_id from [dbo].[columns] where table_id='
        , tabs.[table_id]
        , ')'
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[unique_constraints_columns] where column_id in (select column_id from [dbo].[columns] where table_id='
        , tabs.[table_id]
        , ')'
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[tables_relations_columns] where column_pk_id in (select column_id from [dbo].[columns] where table_id='
        , tabs.[table_id]
        , ') OR column_fk_id in (select column_id from [dbo].[columns] where table_id='
        , tabs.[table_id]
        , ')'
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[erd_nodes] where table_id='
        , tabs.[table_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[tables_relations] where pk_table_id='
        , tabs.[table_id]
        , ' OR fk_table_id='
        , tabs.[table_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[tables_modules] where table_id='
        , tabs.[table_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[tables] where status=''D'' and table_id='
        , tabs.[table_id]
        ) AS [delete command]
FROM [dbo].[tables] tabs
INNER JOIN [dbo].[databases] docs
    ON docs.[database_id] = tabs.[database_id]
WHERE tabs.[status] = 'D'
    AND tabs.[object_type] = 'VIEW'
    AND docs.[title] LIKE @doc_name

UNION ALL

SELECT docs.[title] AS [documentation]
    , 'table' AS [object type]
    , CONCAT (
        tabs.[schema]
        , '.'
        , tabs.[name]
        ) AS [object name]
    , CONCAT (
        'delete from [dbo].[erd_nodes_columns] where column_id in (select column_id from [dbo].[columns] where table_id='
        , tabs.[table_id]
        , ')'
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[unique_constraints_columns] where column_id in (select column_id from [dbo].[columns] where table_id='
        , tabs.[table_id]
        , ')'
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[tables_relations_columns] where column_pk_id in (select column_id from [dbo].[columns] where table_id='
        , tabs.[table_id]
        , ') OR column_fk_id in (select column_id from [dbo].[columns] where table_id='
        , tabs.[table_id]
        , ')'
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[erd_nodes] where table_id='
        , tabs.[table_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[tables_relations] where pk_table_id='
        , tabs.[table_id]
        , ' OR fk_table_id='
        , tabs.[table_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[tables_modules] where table_id='
        , tabs.[table_id]
        , ';'
        , CHAR(13)
        , CHAR(10)
        , 'delete from [dbo].[tables] where status=''D'' and table_id='
        , tabs.[table_id]
        ) AS [delete command]
FROM [dbo].[tables] tabs
INNER JOIN [dbo].[databases] docs
    ON docs.[database_id] = tabs.[database_id]
WHERE tabs.[status] = 'D'
    AND tabs.[object_type] = 'TABLE'
    AND docs.[title] LIKE @doc_name
```