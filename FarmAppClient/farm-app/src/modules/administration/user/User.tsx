import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, { Editing, SearchPanel, Column, RequiredRule, Selection, Sorting, Scrolling, Paging, Pager } from "devextreme-react/tree-list"
import { users } from "../../../api/mock/users"

const allowedPageSizes = [5, 10, 20];

export const User = () => {
    const onCellPrepared = (e: any) => {
        if (e.column.command === 'edit') {
            let addLink = e.cellElement.querySelector('.dx-link-add');

            if (addLink) {
                addLink.remove();
            }
        }
    }

    return (
        <Typography>
            <TreeList
                id="roles"
                dataSource={users}
                showRowLines={true}
                showBorders={true}
                columnAutoWidth={true}
                keyExpr="id"
                onCellPrepared={onCellPrepared}
            >
                <Scrolling mode="standard" />
                <Paging
                    enabled={true}
                    defaultPageSize={5} />
                <Pager
                    showPageSizeSelector={true}
                    allowedPageSizes={allowedPageSizes}
                    showInfo={true} />
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
                    visible={false}
                    dataField={"id"}>
                </Column>
                <Column
                    caption={"Фамилия"}
                    dataField={"firstName"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Имя"}
                    dataField={"userName"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Отчество"}
                    dataField={"lastName"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Имя роли"}
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