Applies to: **Dataedo 24.x (current)** versions, Article available also for: [10.x](https://dataedo.com/docs-10/default-table-colors-on-erds), [23.x](https://dataedo.com/docs-23/default-table-colors-on-erds)

When building bigger ERDs you may want to quickly differentiate tables from outside module and database (documentation). You can use colors for this.

Right now Dataedo does not support this out of the box, but you could set a consistent color scheme using an update statement on the Dataedo repository:

```
UPDATE nods
SET color = CASE 
        WHEN (
                SELECT count(*)
                FROM [dbo].[tables_modules] tab_mods
                WHERE tabs.table_id = tab_mods.table_id
                    AND mods.module_id = tab_mods.module_id
                ) &gt; 0
            THEN '#487AC6' -- color for current module
        WHEN mods.database_id = tabs.database_id
            THEN '#BACE1F' -- color for other modules
        ELSE '#C62D2D' --color for other DBs
        END
FROM [dbo].[erd_nodes] nods
INNER JOIN [dbo].[tables] tabs
    ON nods.table_id = tabs.table_id
INNER JOIN [dbo].[modules] mods
    ON mods.module_id = nods.module_id
INNER JOIN [dbo].[databases] docs
    ON mods.database_id = docs.database_id
-- remove WHERE clause to update the entire repository
WHERE docs.title = 'Documentation title' 
-- remove clause below to update entire documentation
    AND mods.title = 'Module name' 
```

When executed it colors your diagram like this:

![](https://dataedo.com/asset/img/docs/tips_tricks/automatic_coloring_of_erds.png)

You can change default colors by changing RGB codes in the script.

## Default module colors

There's also more advanced technique - you could set a specific color for each of your modules using [custom fields](https://dataedo.com/research-and-development/custom-fields "custom fields") introduced in version 6.0.

You can add a new custom field named **'ERD node color'**, set it applies for modules only:

![](https://dataedo.com/asset/img/docs/tips_tricks/erd_color_custom_field.png)

and provide a color code (RGB hexadecimal format: **#FFFFFF** ) for each module:

![](https://dataedo.com/asset/img/docs/tips_tricks/erd_color_custom_field_value.png)

Then you need to find out the actual table column in which the custom field value is stored using:

```
SELECT [field_name] FROM [dbo].[custom_fields]
   WHERE title = 'ERD color'
```

For me, the result was **field1**. Using it you can just update nodes with colors as defined in modules:

```
UPDATE nods
--change field1 to the result of previous select
SET color = left(mods.field1, 7)
FROM [dbo].[erd_nodes] nods
INNER JOIN [dbo].[tables] tabs
    ON nods.table_id = tabs.table_id
INNER JOIN (
    SELECT table_id
        , min(module_id) module_id
    FROM [dbo].[tables_modules]
    GROUP BY table_id
    ) tab_mods
    ON tabs.table_id = tab_mods.table_id
INNER JOIN [dbo].[modules] mods
    ON mods.module_id = tab_mods.module_id
```

_Do note that if table is in more than one module, color of the older module will be chosen._