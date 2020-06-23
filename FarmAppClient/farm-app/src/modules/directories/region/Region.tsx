import React from "react"
import { Typography } from "@material-ui/core"
import TreeList, {
  Column,
  Editing,
  FilterRow, HeaderFilter,
  Lookup,
  Pager,
  Paging,
  RequiredRule,
  Scrolling,
  SearchPanel,
  Selection,
  Sorting
} from "devextreme-react/tree-list"
import AspNetData from 'devextreme-aspnet-data-nojquery';
import { BASE_URL } from "../../../core/constants";
import { IAppState } from "../../../core/mainReducer";
import { connect } from "react-redux";

const allowedPageSizes = [20, 50, 100];
const expandedRowKeys = [1];


const Region = ({ user }: { user: any }) => {
  const url = `${BASE_URL}api/Regions`;

  const tasksData = AspNetData.createStore({
    key: 'id',
    loadUrl: `${url}?page=1&pageSize=2000`,
    insertUrl: `${url}`,
    updateUrl: `${url}`,
    deleteUrl: `${url}`,
    onBeforeSend: function (method, ajaxOptions) {
      ajaxOptions.xhrFields = { withCredentials: false };
    }
  });


  const regionTypeData = AspNetData.createStore({
    key: 'id',
    loadUrl: `${BASE_URL}api/RegionTypes?page=1&pageSize=2000`
  });

  return (
    <Typography>
      <TreeList
        id="regions"
        //@ts-ignore
        dataSource={tasksData}
        rootValue={0}
        defaultExpandedRowKeys={expandedRowKeys}
        showRowLines={true}
        showBorders={true}
        columnAutoWidth={true}
        parentIdExpr="regionId"
        style={{ maxHeight: '85vh' }}
        keyExpr="id"
        columnHidingEnabled={true}
      >
        <Scrolling mode="standard" />
        <Paging
          enabled={true}
        defaultPageSize={20}
        />
        <Pager
          showPageSizeSelector={true}
          allowedPageSizes={allowedPageSizes}
          showInfo={true} />
        <FilterRow visible={true} />
        <Sorting mode="multiple" />
        <Selection mode="single" />
        <SearchPanel visible={true} />
        <HeaderFilter visible={true} />
        {user?.role?.id === 1 && <Editing
          allowUpdating={true}
          allowDeleting={true}
          allowAdding={true}
          mode="row"
        />
        }
        <Column
          caption={"Номер"}
          dataType={"number"}
          visible={false}
          dataField={"id"}>
        </Column>
        <Column
          alignment={"left"}
          caption={"Название региона"}
          dataType={"string"}
          dataField={"regionName"}>
          <RequiredRule />
        </Column>
        <Column
          alignment={"left"}
          caption={"Тип региона"}
          dataType={"string"}
          dataField={"regionTypeId"}>
          <Lookup dataSource={regionTypeData} valueExpr="id" displayExpr="regionTypeName" />
          <RequiredRule />
        </Column>
        <Column
          alignment={"left"}
          caption={"Численность населения"}
          dataType={"number"}
          dataField={"population"}>
          <RequiredRule />
        </Column>
        {/*<Column*/}
        {/*  caption={"Удалена"}*/}
        {/*  dataType="boolean"*/}
        {/*  dataField={"isDeleted"}>*/}
        {/*</Column>*/}
      </TreeList>
    </Typography>
  )
}


export default connect((state: IAppState) => {
  const { auth } = state;
  return {
    user: auth.user
  }
})(Region)
