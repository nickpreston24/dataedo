Applies to: **Dataedo 24.x (current)** versions, Article available also for: [10.x](https://dataedo.com/docs-10/ignore-undocumented-columns), [23.x](https://dataedo.com/docs-23/ignore-undocumented-columns)

One of our customers has a lot of complex data sources that are mostly undocumented. To make documentation meaningful, they would prefer to leave just the documented columns.

For example, in the screenshot below we'd like to ignore usernames and timestamps of operations on the table.

![Before running scripts](https://dataedo.com/asset/img/docs/6_0/ignore_undocumented_before.png "Before running scripts")

Dataedo doesn't support selection of columns - but we are presenting a workaround. You can **delete columns from the repository** with an SQL script - they won't be available in the UI and exports.

You can always add deleted columns by reimporting database schema.

## Preparation

Import and describe columns you want to document - leave both description and title fields blank for columns you don't want to display.

Open SSMS and connect to your repository database, then run one of the scripts below.

Note that neither script makes any changes to your source database - just to the repository.

## Approach 1: Mark columns as deleted

The script below will mark columns as deleted, but won't acutally remove them from your documentation. Objects marked this way will not be exported to PDF or HTML, but you can still see and edit them in the app.

Script gives effect in UI as on the screenshot below:

![After script 1 - editor](https://dataedo.com/asset/img/docs/6_0/ignore_undocumented_after_1_editor.png "After script 1 - editor")

And the columns don't show up in exported documentation (PDF, HTML or Excel):

![After script 1 - export](https://dataedo.com/asset/img/docs/6_0/ignore_undocumented_after_export.png "After script 1 - export")

Before running the script, replace _Your\_documentation\_title_ with the title of documentation you want to edit.

```
UPDATE [dbo].[columns]
SET STATUS = 'D'
WHERE column_id IN (
        SELECT column_id
        FROM [dbo].[columns] cols
        INNER JOIN [dbo].[tables] tabs
            ON cols.table_id = tabs.table_id
        INNER JOIN [dbo].[databases] docs
            ON tabs.database_id = docs.database_id
        WHERE (
                cols.[description] IS NULL
                OR cols.[description] = ''
                )
            AND (
                cols.[title] IS NULL
                OR cols.[title] = ''
                )
            AND docs.title = 'Your_documentation_title'
        );
```

## Approach 2: Remove undocumented columns

The script below will actually delete columns without a description or title from repository. Use this option if you want to remove clutter not only from exports, but also from the app.

We do not recommend using this approach to remove columns that are parts of primary, unique or foreign keys, as this will remove columns not only from column list, but also from unique keys and relations - instead they will show a blank field in their column lists or join conditions.

After running the script, the table looks like this in Dataedo (note that columns are gone):

![After script 2 - editor](https://dataedo.com/asset/img/docs/6_0/ignore_undocumented_after_2_editor.png "After script 2 - editor")

And documentation export looks the same as in approach one in column list:

![After script 2 - export](https://dataedo.com/asset/img/docs/6_0/ignore_undocumented_after_export.png "After script 2 - export")

Before running the script subsitute _Your\_documentation\_title_ with the title of documentation you want to edit.

```
DECLARE @doc_title NVARCHAR(1024) = 'Your_documentation_title'
    , @doc_id INT;

SET @doc_id = (
        SELECT TOP 1 [database_id]
        FROM [dbo].[databases]
        WHERE [title] = @doc_title
        );

DELETE
FROM [dbo].[unique_constraints_columns]
WHERE unique_constraint_id IN (
        SELECT concols.unique_constraint_id
        FROM [dbo].[columns] cols
        INNER JOIN [dbo].[tables] tabs
            ON cols.table_id = tabs.table_id
        INNER JOIN [dbo].[unique_constraints_columns] concols
            ON concols.column_id = cols.column_id
        WHERE (
                cols.[description] IS NULL
                OR cols.[description] = ''
                )
            AND (
                cols.[title] IS NULL
                OR cols.[title] = ''
                )
            AND tabs.database_id = @doc_id
        );

DELETE
FROM [dbo].[tables_relations_columns]
WHERE table_relation_id IN (
        SELECT relcols.table_relation_id
        FROM [dbo].[columns] cols
        INNER JOIN [dbo].[tables] tabs
            ON cols.table_id = tabs.table_id
        INNER JOIN [dbo].[tables_relations_columns] relcols
            ON relcols.column_fk_id = cols.column_id
                OR relcols.column_pk_id = cols.column_id
        WHERE (
                cols.[description] IS NULL
                OR cols.[description] = ''
                )
            AND (
                cols.[title] IS NULL
                OR cols.[title] = ''
                )
            AND tabs.database_id = @doc_id
        );

DELETE
FROM [dbo].[erd_nodes_columns]
WHERE column_id IN (
        SELECT column_id
        FROM [dbo].[columns] cols
        INNER JOIN [dbo].[tables] tabs
            ON cols.table_id = tabs.table_id
        WHERE (
                cols.[description] IS NULL
                OR cols.[description] = ''
                )
            AND (
                cols.[title] IS NULL
                OR cols.[title] = ''
                )
            AND tabs.database_id = @doc_id
        );

DELETE
FROM [dbo].[columns]
WHERE column_id IN (
        SELECT column_id
        FROM [dbo].[columns] cols
        INNER JOIN [dbo].[tables] tabs
            ON cols.table_id = tabs.table_id
        WHERE (
                cols.[description] IS NULL
                OR cols.[description] = ''
                )
            AND (
                cols.[title] IS NULL
                OR cols.[title] = ''
                )
            AND tabs.database_id = @doc_id
        );
```

## Bringing back deleted columns

When you want to bring back deleted columns simply perform a documentation update with the **[reimport all included objects](https://dataedo.com/docs/reimport-all-objects "Reimport all included objects")** option selected. This will restore all your columns.

Then, you can add descriptions to new columns rerun the script again if you wish.