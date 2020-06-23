import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, {
    Editing,
    SearchPanel,
    Column,
    RequiredRule,
    Selection,
    Sorting,
    Scrolling,
    Paging,
    Pager,
    HeaderFilter,
    Lookup,
    FilterRow
} from "devextreme-react/tree-list"
import { sales } from "../../api/mock/sales"
import { BASE_URL } from "../../core/constants";
import AspNetData from "devextreme-aspnet-data-nojquery";
import { IAppState } from "../../core/mainReducer";
import { connect } from "react-redux";

const allowedPageSizes = [20, 50, 100];

const Sales = ({ user }: { user: any }) => {
    // const allowedPageSizes = [5, 10, 20];
    // const onCellPrepared = (e: any) => {
    //     if (e.column.command === 'edit') {
    //         let addLink = e.cellElement.querySelector('.dx-link-add');
    //
    //         if (addLink) {
    //             addLink.remove();
    //         }
    //     }
    // }


    const salesData = AspNetData.createStore({
        key: 'id',
        loadUrl: `${BASE_URL}api/Sales?page=1&pageSize=10000`,
        insertUrl: `${BASE_URL}api/Sales`,
        updateUrl: `${BASE_URL}api/Sales`,
        deleteUrl: `${BASE_URL}api/Sales`,
        onBeforeSend: function (method, ajaxOptions) {
            ajaxOptions.xhrFields = { withCredentials: false };
        }
    });

    const drugsData = AspNetData.createStore({
        key: 'id',
        loadUrl: `${BASE_URL}api/Drugs?page=1&pageSize=2000`
    });

    const pharmacyData = AspNetData.createStore({
        key: 'id',
        loadUrl: `${BASE_URL}api/Pharmacies?page=1&pageSize=2000`
    });


    return (
        <Typography>
            <TreeList
                id="sales"
                //@ts-ignore
                dataSource={salesData}
                showRowLines={true}
                showBorders={true}
                columnAutoWidth={true}
                keyExpr="id"
                columnHidingEnabled={true}
                style={{ height: '85vh' }}
            // onCellPrepared={onCellPrepared}
            >
                <HeaderFilter visible={true} />
                <Scrolling mode="standard" />
                <FilterRow visible={true} />
                <Paging
                    enabled={true}
                defaultPageSize={20}
                />
                <Pager
                    showPageSizeSelector={true}
                    allowedPageSizes={allowedPageSizes}
                    showInfo={true} />
                <Sorting mode="multiple" />
                <Selection mode="single" />
                <SearchPanel visible={true} />
                {user?.role?.id === 1 && <Editing
                    allowUpdating={true}
                    allowDeleting={true}
                    allowAdding={true}
                    mode="row"
                />
                }
                <Column
                    caption={"Номер"}
                    visible={false}
                    dataField={"id"}>
                </Column>
                <Column
                    caption={"Название препарата"}
                    dataField={"drugId"}>
                    <Lookup dataSource={drugsData} valueExpr="id" displayExpr="drugName" />
                    <RequiredRule />
                </Column>
                <Column
                    caption={"Название аптеки"}
                    dataField={"pharmacyId"}>
                    <Lookup dataSource={pharmacyData} valueExpr="id" displayExpr="pharmacyName" />
                    <RequiredRule />
                </Column>

                <Column
                    // alignment="right"
                    dataType="date"
                    // allowHeaderFiltering={false}
                    caption={"Дата продажи"}
                    dataField={"saleDate"}
                    format={'dd.MM.YYYY HH:mm'}
                >
                    <RequiredRule />
                </Column>
                <Column
                    dataField={"price"}
                    alignment="left"
                    // allowHeaderFiltering={false}
                    caption={"Цена за ед."}
                    format={"#,##0.00"}
                >
                    <RequiredRule />
                </Column>
                <Column
                    // allowHeaderFiltering={false}
                    alignment="left"
                    caption={"Кол-во"}
                    dataField={"quantity"}>
                    <RequiredRule />
                </Column>
                <Column
                    alignment="left"
                    // allowHeaderFiltering={false}
                    caption={"Сумма"}
                    dataField={"amount"}
                    format={"#,##0.00"}
                >
                    <RequiredRule />
                </Column>
                <Column
                    // allowHeaderFiltering={false}
                    alignment="left"
                    caption={"Дисконт"}
                    dataType="boolean"
                    dataField={"isDiscount"}>
                </Column>
                <Column
                    // allowHeaderFiltering={false}
                    alignment="left"
                    caption={"Удалена"}
                    dataType="boolean"
                    dataField={"isDeleted"}
                    visible={false}
                >
                </Column>
            </TreeList>
        </Typography>
    )
}


export default connect((state: IAppState) => {
    const { auth } = state;
    return {
        user: auth.user
    }
})(Sales)
