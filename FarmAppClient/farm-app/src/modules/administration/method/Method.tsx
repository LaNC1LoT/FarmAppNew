import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, { Editing, SearchPanel, Column, RequiredRule, Selection, Sorting, Scrolling, Paging, Pager, FilterRow } from "devextreme-react/tree-list"
import { apimethods } from "../../../api/mock/apimethod"

const allowedPageSizes = [5, 10, 20];

export const Method = () => {
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
                id="apimethods"
                dataSource={apimethods}
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
                    dataType={"number"}
                    visible={false}
                    dataField={"id"}>
                </Column>
                <Column
                    caption={"Имя метода"}
                    dataType={"string"}
                    dataField={"name"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Описание"}
                    dataType={"string"}
                    dataField={"discription"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Короткий адрес"}
                    dataType={"string"}
                    dataField={"pathUtl"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Http Метод"}
                    dataType={"string"}
                    dataField={"httpMethod"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Параметры"}
                    dataField={"isNotNullParam"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Аунтификация"}
                    dataField={"isNeedAuntification"}>
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