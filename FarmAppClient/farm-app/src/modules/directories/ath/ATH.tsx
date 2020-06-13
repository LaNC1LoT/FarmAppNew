import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, { Editing, SearchPanel, Column, RequiredRule, Selection, Sorting, FilterRow, Pager, Paging, Scrolling } from "devextreme-react/tree-list"
import { codeAthType } from "../../../api/mock/codeAthType"

const allowedPageSizes = [5, 10, 20];

export const ATH = () => {
    return (
        <Typography>
            <TreeList
                id="codeAthType"
                dataSource={codeAthType}
                showRowLines={true}
                showBorders={true}
                columnAutoWidth={true}
                keyExpr="id"
                rootValue={1}
                autoExpandAll={true}
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
                    caption={"Название группы"}
                    dataType={"string"}
                    dataField={"name"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Код группы"}
                    dataType={"string"}
                    dataField={"code"}>
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