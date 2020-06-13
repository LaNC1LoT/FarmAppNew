import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, { Editing, SearchPanel, Column, RequiredRule, Selection, Sorting, Scrolling, Paging, Pager, FilterRow } from "devextreme-react/tree-list"
import { preparations } from "../../../api/mock/preparations"

export const Preparations = () => {
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
                id="preparations"
                dataSource={preparations}
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
                <FilterRow visible={true} />
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
                    caption={"Имя группы Атх"}
                    dataField={"ath.name"}>
                    <RequiredRule />
                </Column>

                <Column
                    caption={"Код группы Атх"}
                    dataField={"ath.code"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Имя производителя"}
                    dataField={"vendor.name"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Отечесвтенный"}
                    dataField={"isDomestic"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Generic"}
                    dataField={"isGeneric"}>
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