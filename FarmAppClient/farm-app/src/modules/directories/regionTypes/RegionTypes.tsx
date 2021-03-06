import React, { useEffect } from "react"
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


const RegionTypes = ({ user }: { user: any }) => {
  const url = `${BASE_URL}api/RegionTypes`;
  const userData: any = localStorage.getItem('auth')
  const token: any = userData ? JSON.parse(userData).token : user?.token ?? ""
  const regionType = AspNetData.createStore({
    key: 'id',
    loadUrl: `${url}?page=1&pageSize=2000`,
    insertUrl: `${url}`,
    updateUrl: `${url}`,
    deleteUrl: `${url}`,
    onBeforeSend: function (method, ajaxOptions) {
      // ajaxOptions.xhrFields = { withCredentials: false };
      ajaxOptions.headers = {
        Authorization: 'Bearer ' + token,
      };
    }
  });
  useEffect(() => {
    document.title = 'Тип региона'
  })

  // const regionTypeData = AspNetData.createStore({
  //   key: 'id',
  //   loadUrl: `${BASE_URL}api/regionTypes?page=1&pageSize=2000`
  // });
  const onCellPrepared = (e: any) => {
    if (e.column.command === 'edit') {
      let addLink = e.cellElement.querySelector('.dx-link-add');

      if (addLink) {
        addLink.remove();
      }
    }
  };

  return (
    <Typography>
      <TreeList
        id="regionType"
        //@ts-ignore
        dataSource={regionType}
        rootValue={0}
        defaultExpandedRowKeys={expandedRowKeys}
        showRowLines={true}
        showBorders={true}
        columnAutoWidth={true}
        parentIdExpr="regionId"
        style={{ maxHeight: '85vh' }}
        keyExpr="id"
        columnHidingEnabled={true}
        onCellPrepared={onCellPrepared}
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
        {user?.role?.id !== 2 && <Editing
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
          caption={"Тип региона"}
          dataType={"string"}
          dataField={"regionTypeName"}>
          <RequiredRule />
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
})(RegionTypes)
