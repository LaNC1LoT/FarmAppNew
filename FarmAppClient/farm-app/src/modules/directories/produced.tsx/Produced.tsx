import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, { Editing, SearchPanel, Column, RequiredRule, Selection, Sorting, Scrolling, Paging, Pager } from "devextreme-react/tree-list"
import { vendors } from "../../../api/mock/vendors"

export const Produced = () => {
    const allowedPageSizes = [5, 10, 20];
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
                id="vendors"
                dataSource={vendors}
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
                    caption={"Имя производителя"}
                    dataField={"name"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Отечесвтенный"}
                    dataField={"isDomestic"}>
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