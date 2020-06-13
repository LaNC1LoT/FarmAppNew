import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, { Editing, SearchPanel, Column, RequiredRule, Selection, Sorting } from "devextreme-react/tree-list"
import { roles } from "../../../api/mock/roles"

export const Role = () => {
    return (
        <Typography>
            <TreeList
                id="roles"
                dataSource={roles}
                showRowLines={true}
                showBorders={true}
                columnAutoWidth={true}
                keyExpr="id"
            >
                <Sorting mode="multiple" />
                <Selection mode="single" />
                <SearchPanel visible={true} />
                <Editing
                    allowUpdating={true}
                    allowDeleting={true}
                    mode="row"
                />
                <Column
                    caption={"Номер"}
                    dataType={"number"}
                    visible={false}
                    dataField={"id"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Имя роли"}
                    dataType={"string"}
                    dataField={"name"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Удалена"}
                    dataType="boolean"
                    dataField={"isDeleted"}>
                </Column>
            </TreeList>
        </Typography>
    )
}