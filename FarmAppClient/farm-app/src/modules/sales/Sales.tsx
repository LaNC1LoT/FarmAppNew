import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, { Editing, SearchPanel, Column, RequiredRule, Selection, Sorting, Scrolling, Paging, Pager, HeaderFilter } from "devextreme-react/tree-list"
import { sales } from "../../api/mock/sales"

export const Sales = () => {
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
                id="sales"
                dataSource={sales}
                showRowLines={true}
                showBorders={true}
                columnAutoWidth={true}
                keyExpr="id"
                onCellPrepared={onCellPrepared}
            >
                <HeaderFilter visible={true} />
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
                    caption={"Название препарата"}
                    dataField={"drug.name"}>
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Название аптеки"}
                    dataField={"pharmacy.name"}>
                    <RequiredRule />
                </Column>
                <Column
                    alignment="right"
                    dataType="date"
                    allowHeaderFiltering={false}
                    caption={"Дата продажи"}
                    dataField={"date"}>
                    <RequiredRule />
                </Column>
                <Column
                    allowHeaderFiltering={false}
                    caption={"Цена за ед."}
                    dataField={"price"}>
                    <RequiredRule />
                </Column>
                <Column
                    allowHeaderFiltering={false}
                    caption={"Кол-во"}
                    dataField={"quantity"}>
                    <RequiredRule />
                </Column>
                <Column
                    allowHeaderFiltering={false}
                    caption={"Сумма"}
                    dataField={"amount"}>
                    <RequiredRule />
                </Column>
                <Column
                    allowHeaderFiltering={false}
                    caption={"Дисконт"}
                    dataType="boolean"
                    dataField={"isDiscount"}>
                </Column>
                <Column
                    allowHeaderFiltering={false}
                    caption={"Удалена"}
                    dataType="boolean"
                    dataField={"isDeleted"}>
                </Column>
            </TreeList>
        </Typography>
    )
}
