Applies to: **Dataedo 24.x (current)** versions, Article available also for: [10.x](https://dataedo.com/docs-10/assigning-tables-to-module-automatically-based-on-their-name), [23.x](https://dataedo.com/docs-23/assigning-tables-to-module-automatically-based-on-their-name)

You can use SQL query below if you would like to automatically assign tables to certain module based on their name.

```
INSERT INTO dbo.tables_modules (
    table_id
    , module_id
    )
SELECT tab.table_id
    , m.module_id
FROM dbo.[tables] tab
INNER JOIN dbo.[databases] d
    ON tab.database_id = d.database_id
LEFT JOIN dbo.modules m
    ON tab.database_id = m.database_id
LEFT JOIN dbo.tables_modules mod_tab
    ON tab.table_id = mod_tab.table_id
        AND m.module_id = mod_tab.module_id
WHERE mod_tab.table_id IS NULL
    AND (
        -- define table rules here
        (tab.[name] LIKE 'hr%'
            OR tab.[schema] = 'HumanResources')
        AND tab.[object_type] = 'TABLE' -- or 'VIEW', or remove to copy both tables and views
        )
    AND
    -- define module rules here
    (m.title = 'Module Title')
    -- remove clause below if you want to 
    -- apply this rule for all documentations in a repository
    AND d.title = 'Documentation title'

```

**Parameters**

-   **Module title** - Above SQL query assigns tables to specific module, you must paste its title into **'Module title'** string,
-   **Documentation title** - If you want to perform execute rule for specific documentation within repository provide its title in place of 'Documentation title'. If you want to execute it for entire repository remove this line,
-   **Rule** - You can define your rule for which tables to assign to a specific module using SQL _where_ clause and fields **name**, **schema** or any other table field.

See [repository database specification](https://dataedo.com/docs/repository-database-schema)

**Views**

If you want to apply the rule to views you can change clause **tab.\[object\_type\] = 'TABLE'** to **tab.\[object\_type\] = 'VIEW'** (or remove the clause to assign both tables and views).

**Stored procedures and functions**

If you want to perform the same for procedures or functions replace **dbo.tables** with **dbo.procedures** and **dbo.tables\_modules** with **dbo.procedures\_modules**.

**object\_type** column indicates whether it's function ('FUNCTION') or stored procedure ('PROCEDURE').