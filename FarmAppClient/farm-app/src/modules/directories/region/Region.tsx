import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, { Editing, SearchPanel, Column, RequiredRule, Selection, Sorting, FilterRow, Pager, Paging, Scrolling } from "devextreme-react/tree-list"
import { regions } from "../../../api/mock/region"

const allowedPageSizes = [5, 10, 20];
const expandedRowKeys = [1];

export const Region = () => {
    return (
        <Typography>
            <TreeList
                id="regions"
                dataSource={regions}
                rootValue={-1}
                defaultExpandedRowKeys={expandedRowKeys}
                showRowLines={true}
                showBorders={true}
                columnAutoWidth={true}
                parentIdExpr="parentId"
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
                    caption={"Название региона"}
                    dataType={"string"}
                    dataField={"name"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Численность населения"}
                    dataType={"number"}
                    dataField={"population"}>
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