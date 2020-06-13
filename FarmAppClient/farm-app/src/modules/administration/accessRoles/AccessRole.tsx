import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, { Editing, SearchPanel, Column, RequiredRule, Selection, Sorting } from "devextreme-react/tree-list"
import { apimethodroles } from "../../../api/mock/apimethodroles"

export const AccessRole = () => {
    const onCellPrepared = (e: any) => {
        if (e.column.command === 'edit') {
            let addLink = e.cellElement.querySelector('.dx-link-add');

            if (addLink) {
                addLink.remove();
            }
        }
    }
    return(
        <Typography>
        <TreeList
            id="apimethodroles"
            dataSource={apimethodroles}
            showRowLines={true}
            showBorders={true}
            columnAutoWidth={true}
            keyExpr="id"
            onCellPrepared={onCellPrepared}
        >
            <Sorting mode="multiple" />
            <Selection mode="single" />
            <SearchPanel visible={true} />
            <Editing
                allowUpdating={true}
                allowDeleting={true}
                allowAdding={true}
                mode="row"
            />
            <Column
                caption={"Номер"}
                dataType={"number"}
                visible={false}
                dataField={"id"}>
            </Column>
            <Column
                caption={"Метод"}
                dataType={"string"}
                dataField={"apiMethod.name"}>
                <RequiredRule />
            </Column>
            <Column
                caption={"Роль"}
                dataType={"string"}
                dataField={"role.name"}>
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