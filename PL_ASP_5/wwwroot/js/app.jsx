class Vehicle extends React.Component {

    constructor(props) {
        super(props);
        this.state = { data: props.vehicle };
        this.onClick = this.onClick.bind(this);
    }
    onClick(e) {
        this.props.onRemove(this.state.data);
    }
    render() {
        return <div>
            <p>Номер гос. регистрации<b>{this.state.data.registrationNumber}</b></p>
            <p>Дата ТО {this.state.data.maintenanceDate}</p>
            <p><button onClick={this.onClick}>Удалить</button></p>
        </div>;
    }
}

class VehicleForm extends React.Component {

    constructor(props) {
        super(props);
        this.state = { registrationNumber: "", maintenanceDate: 0 };

        this.onSubmit = this.onSubmit.bind(this);
        this.onNumberChange = this.onNumberChange.bind(this);
        this.onDateChange = this.onDateChange.bind(this);
    }
    onNumberChange(e) {
        this.setState({ registrationNumber: e.target.value });
    }
    onDateChange(e) {
        this.setState({ maintenanceDate: e.target.value });
    }
    onSubmit(e) {
        e.preventDefault();
        var vehicleNumber = this.state.registrationNumber.trim();
        var vehicleDate = this.state.maintenanceDate;
        if (!vehicleNumber || vehicleDate <= 0) {
            return;
        }
        this.props.onVehicleSubmit({ registrationNumber: vehicleNumber, maintenanceDate: vehicleDate });
        this.setState({ registrationNumber: "", maintenanceDate: "" });
    }
    render() {
        return (
            <form onSubmit={this.onSubmit}>
                <p>
                    <input type="text"
                        placeholder="Номер гос. регистрации"
                        value={this.state.registrationNumber}
                        onChange={this.onNumberChange} />
                </p>
                <p>
                    <input type="number"
                        placeholder="Дата ТО"
                        value={this.state.maintenanceDate}
                        onChange={this.onDateChange} />
                </p>
                <input type="submit" value="Сохранить" />
            </form>
        );
    }
}


class VehicleList extends React.Component {

    constructor(props) {
        super(props);
        this.state = { vehicles: [] };

        this.onAddVehicle = this.onAddVehicle.bind(this);
        this.onRemoveVehicle = this.onRemoveVehicle.bind(this);
    }
    // загрузка данных
    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.apiUrl, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ vehicles: data });
        }.bind(this);
        xhr.send();
    }
    componentDidMount() {
        this.loadData();
    }
    // добавление объекта
    onAddVehicle(vehicle) {
        if (vehicle) {

            const data = new FormData();
            data.append("registrationNumber", vehicle.registrationNumber);
            data.append("maintenanceDate", vehicle.maintenanceDate);
            data.append("maintenanceSuccess", vehicle.maintenanceSuccess);
            var xhr = new XMLHttpRequest();

            xhr.open("post", this.props.apiUrl, true);
            xhr.onload = function () {
                if (xhr.status === 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send(data);
        }
    }
    // удаление объекта
    onRemoveVehicle(vehicle) {

        if (vehicle) {
            var url = this.props.apiUrl + "/" + vehicle.id;

            var xhr = new XMLHttpRequest();
            xhr.open("delete", url, true);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onload = function () {
                if (xhr.status === 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send();
        }
    }
    render() {

        var remove = this.onRemoveVehicle;
        return <div>
            <VehicleForm onVehicleSubmit={this.onAddVehicle} />
            <h2>Список транспортных средств</h2>
            <div>
                {
                    this.state.vehicles.map(function (vehicle) {

                        return <Vehicle key={vehicle.id} vehicle={vehicle} onRemove={remove} />
                    })
                }
            </div>
        </div>;
    }
}

ReactDOM.render(
    <VehicleList apiUrl="/api/Vehicles" />,
    document.getElementById("content")
);