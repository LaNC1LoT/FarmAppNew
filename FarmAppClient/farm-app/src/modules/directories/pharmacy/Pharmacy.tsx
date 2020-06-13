import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, { Editing, SearchPanel, Column, RequiredRule, Selection, Sorting, FilterRow, Pager, Paging, Scrolling } from "devextreme-react/tree-list"
import { pharmacies } from "../../../api/mock/pharmacies"

export const Pharmacy = () => {
    const allowedPageSizes = [5, 10, 20];

    return (
        <Typography>
            <TreeList
                id="pharmacies"
                dataSource={pharmacies}
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
                    caption={"Название аптеки"}
                    dataType={"string"}
                    dataField={"name"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Имя региона"}
                    dataType={"string"}
                    dataField={"region.name"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Круглосуточная"}
                    dataType="boolean"
                    dataField={"isMode"}>
                </Column>
                <Column
                    caption={"Социальная"}
                    dataType="boolean"
                    dataField={"isType"}>
                </Column>
                <Column
                    caption={"Сеть аптек"}
                    dataType="boolean"
                    dataField={"isNetwork"}>
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